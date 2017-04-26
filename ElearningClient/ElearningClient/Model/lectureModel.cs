using System;

namespace ElearningClient.Model
{
    public enum LECTURE_TYPE
    {
        HAND_WRITING = 0,
        DOCUMENT_VIEW
    }

    public class lectureModel
	{
        public teacherInformation teacher { get; set; }
        public string lectureName { get; set ; }
		public string lectureComment { get; set; }
		public LECTURE_TYPE lectureType { get; set; }
        public string documentPath { get; set; }
        public string audioPath { get; set; }
        public string lectureDuration { get; set; }
		public lectureModel ()
		{
		}
	}

    public class lectureBindingModel
    {
        public lectureModel lecture;
        public string lectureName { get; set; }
        public string lectureComment { get; set; }
        public string lectureImagePath { get; set; }
        public lectureBindingModel(lectureModel l)
        {
            lecture = l;
            lectureName = l.lectureName;
            lectureComment = l.lectureComment;
            if (l.lectureType == LECTURE_TYPE.HAND_WRITING)
                lectureImagePath = "hand.png";
            else
                lectureImagePath = "pdf.png";
        }
    }
    public class teacherInformation
    {
        public string teacherName { get; set; }
        public string teacherImage { get; set; }
        public string comment { get; set; }
    }
}

