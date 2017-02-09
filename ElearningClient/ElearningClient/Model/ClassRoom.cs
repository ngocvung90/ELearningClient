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
    public partial class ClassRoom
    {

        private ClassRoomClassInfor classInforField;

        private ClassRoomDetail[] lectureDetailField;

        /// <remarks/>
        public ClassRoomClassInfor ClassInfor
        {
            get
            {
                return this.classInforField;
            }
            set
            {
                this.classInforField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Detail", IsNullable = false)]
        public ClassRoomDetail[] LectureDetail
        {
            get
            {
                return this.lectureDetailField;
            }
            set
            {
                this.lectureDetailField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ClassRoomClassInfor
    {

        private ClassRoomClassInforTeacher teacherField;

        private string subjectNameField;

        private ClassRoomClassInforDocumentList documentListField;

        /// <remarks/>
        public ClassRoomClassInforTeacher Teacher
        {
            get
            {
                return this.teacherField;
            }
            set
            {
                this.teacherField = value;
            }
        }

        /// <remarks/>
        public string SubjectName
        {
            get
            {
                return this.subjectNameField;
            }
            set
            {
                this.subjectNameField = value;
            }
        }

        /// <remarks/>
        public ClassRoomClassInforDocumentList DocumentList
        {
            get
            {
                return this.documentListField;
            }
            set
            {
                this.documentListField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ClassRoomClassInforTeacher
    {

        private string nameField;

        private object avatarField;

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public object Avatar
        {
            get
            {
                return this.avatarField;
            }
            set
            {
                this.avatarField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ClassRoomClassInforDocumentList
    {

        private string documentField;

        /// <remarks/>
        public string Document
        {
            get
            {
                return this.documentField;
            }
            set
            {
                this.documentField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ClassRoomDetail
    {

        private byte timeField;

        private string documentField;

        private byte pageField;

        /// <remarks/>
        public byte Time
        {
            get
            {
                return this.timeField;
            }
            set
            {
                this.timeField = value;
            }
        }

        /// <remarks/>
        public string Document
        {
            get
            {
                return this.documentField;
            }
            set
            {
                this.documentField = value;
            }
        }

        /// <remarks/>
        public byte Page
        {
            get
            {
                return this.pageField;
            }
            set
            {
                this.pageField = value;
            }
        }
    }


}
