using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sockets.Plugin.Abstractions;

public static class SocketExtensions
{
    /// <summary>
    /// Continuously read strings from the socketclient and yield them as <code>Message</code> instances.
    /// Stops when <code>eof</code> is hit. Messages are delimited by <code>eom</code>.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="eom"></param>
    /// <param name="eof"></param>
    /// <returns></returns>
    public static IEnumerable<Message> ReadStrings(this ITcpSocketClient client, CancellationToken cancellationToken, string eom = "EOM", string eof = "EOF")
    {
        var from = String.Format("{0}:{1}", client.RemoteAddress, client.RemotePort);

        var currData = "";
        int bytesRec = 0;
        bool gotEof = false;
        bool gotEOM = false;
            var buffer = new byte[1024];
        while (bytesRec != -1 && !cancellationToken.IsCancellationRequested && !gotEof)
        {
            bytesRec = client.ReadStream.Read(buffer, 0, buffer.Length);
            if (bytesRec == 0) continue;
            currData += Encoding.UTF8.GetString(buffer, 0, bytesRec);

            // Hit an EOM - we have a full message in currData;
            int indexEOM = currData.IndexOf(eom);
            if (currData.IndexOf(eom) > -1)
            {
                var msg = new Message
                {
                    Text = currData.Substring(0, currData.IndexOf(eom)),
                    DetailText = String.Format("<Received from Vung9 {0} at {1}>", from, DateTime.Now.ToString("HH:mm:ss"))
                };

                yield return msg;

                System.Diagnostics.Debug.WriteLine("Received data, , data length : {0} buffer length : {1}, from {2} : {3}", currData, currData.Length, buffer.Length, from);
                currData = currData.Substring(currData.IndexOf(eom) + eom.Length);
                currData = "";
                gotEOM = true;
            }

            int indexEOF = currData.IndexOf(eof);
            // Hit an EOF - client is gracefully disconnecting
            if (currData != "" && indexEOF > -1)
            {
                var msg = new Message
                {
                    DetailText = String.Format("<{0} disconnected at {1}>", from, DateTime.Now.ToString("HH:mm:ss"))
                };

                yield return msg;
                System.Diagnostics.Debug.WriteLine("Received data 3gp, data : {0}", currData);
                currData = "";
                //gotEof = true;
                gotEOM = false;
            }
        }
        yield return new Message();

        // if we get here, either the stream broke, the cancellation token was cancelled, or the eof message was received
        // time to drop the client.

        try
        {
            client.DisconnectAsync().Wait();
            client.Dispose();
        }
        catch { }
    }

    /// <summary>
    /// Writes a string to a socket client stream in UT8 with the specified eom identifier
    /// </summary>
    /// <param name="client"></param>
    /// <param name="s"></param>
    /// <param name="eom"></param>
    /// <returns></returns>
    public async static Task WriteStringAsync(this ITcpSocketClient client, string s, string eom = "EOM")
    {
        var bytes = (s + eom).ToUTF8Bytes();

        await client.WriteStream.WriteAsync(bytes, 0, bytes.Length);
        await client.WriteStream.FlushAsync();
    }

    public static byte[] ToUTF8Bytes(this string s)
    {
        return Encoding.UTF8.GetBytes(s);
    }

    public static string ToStringFromUTF8Bytes(this byte[] buf)
    {
        return Encoding.UTF8.GetString(buf, 0, buf.Length);
    }

};