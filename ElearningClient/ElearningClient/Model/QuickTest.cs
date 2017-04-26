using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElearningClient.Model
{

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class QuickTest
    {

        private QuickTestInformation informationField;

        private QuickTestQuiz[] detailField;

        /// <remarks/>
        public QuickTestInformation Information
        {
            get
            {
                return this.informationField;
            }
            set
            {
                this.informationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Quiz", IsNullable = false)]
        public QuickTestQuiz[] Detail
        {
            get
            {
                return this.detailField;
            }
            set
            {
                this.detailField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class QuickTestInformation
    {

        private string lectureField;

        private ushort testTimeField;

        private byte numberQuestionField;

        /// <remarks/>
        public string Lecture
        {
            get
            {
                return this.lectureField;
            }
            set
            {
                this.lectureField = value;
            }
        }

        /// <remarks/>
        public ushort TestTime
        {
            get
            {
                return this.testTimeField;
            }
            set
            {
                this.testTimeField = value;
            }
        }

        /// <remarks/>
        public byte NumberQuestion
        {
            get
            {
                return this.numberQuestionField;
            }
            set
            {
                this.numberQuestionField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class QuickTestQuiz
    {

        private byte idField;

        private string contentField;

        private QuickTestQuizAns ansField;

        /// <remarks/>
        public byte ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public string Content
        {
            get
            {
                return this.contentField;
            }
            set
            {
                this.contentField = value;
            }
        }

        /// <remarks/>
        public QuickTestQuizAns Ans
        {
            get
            {
                return this.ansField;
            }
            set
            {
                this.ansField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class QuickTestQuizAns
    {

        private ushort[] aField;

        private ushort correctField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("A")]
        public ushort[] A
        {
            get
            {
                return this.aField;
            }
            set
            {
                this.aField = value;
            }
        }

        /// <remarks/>
        public ushort Correct
        {
            get
            {
                return this.correctField;
            }
            set
            {
                this.correctField = value;
            }
        }
    }


}
