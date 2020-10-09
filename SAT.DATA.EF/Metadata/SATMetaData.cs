using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SAT.DATA.EF//.Metadata
{
    #region Course Metadata

    public class CourseMetaData
    {
        //public int CouseId { get; set; }
        [Display(Name = "Course Name")]
        [Required(ErrorMessage = "* Required")]
        [StringLength(50, ErrorMessage = "* Must be 50 characters or less.")]

        public string CourseName { get; set; }

        [Display(Name = "Course Description")]
        [Required(ErrorMessage = "* Required")]
        public string CourseDescription { get; set; }

        [Display(Name = "Credit Hours")]
        [Required(ErrorMessage = "* Required")]
        public byte CreditHours { get; set; }

        [Display(Name = "Curriculum")]
        [StringLength(250, ErrorMessage = "* Must be 250 characters or less.")]
        public string Curriculum { get; set; }

        [Display(Name = "Notes")]
        [StringLength(500, ErrorMessage = "* Must be 500 characters or less.")]
        public string Notes { get; set; }

        [Display(Name = "Active Course")]
        [Required(ErrorMessage = "* Required")]
        public bool IsActive { get; set; }
    }
    [MetadataType(typeof(CourseMetaData))]
    public partial class Course { }

    #endregion

    #region Enrollment Metadata

    public class EnrollmentMetaData
    {
        [Required(ErrorMessage = "* Required")]
        public int EnrollmentID { get; set; }

        [Required(ErrorMessage = "* Required")]
        public int StudentID { get; set; }

        [Required(ErrorMessage = "* Required")]
        public int ScheduledClassId { get; set; }

        [Display(Name = "Date Enrolled")]
        [Required(ErrorMessage = "* Required")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true, NullDisplayText = "NA")]
        public System.DateTime EnrollmentDate { get; set; }
    }
    [MetadataType(typeof(EnrollmentMetaData))]
    public partial class Enrollment { }

    #endregion

    #region ScheduledClass Metadata

    public class ScheduledClassMetaData
    {
        //public int ScheduledClassId { get; set; }
        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Course")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true, NullDisplayText = "NA")]
        public System.DateTime StartDate { get; set; }

        [Required(ErrorMessage = "* Required")]
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true, NullDisplayText = "NA")]
        public System.DateTime EndDate { get; set; }

        [Required(ErrorMessage = "* Required")]
        [StringLength(40, ErrorMessage = "* Must be 40 characters or less.")]
        [Display(Name = "Instructor")]
        public string InstructorName { get; set; }

        [Required(ErrorMessage = "* Required")]
        [StringLength(20, ErrorMessage = "* Must be 20 characters or less.")]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Required(ErrorMessage = "* Required")]
        public int SCSID { get; set; }
    }
    [MetadataType(typeof(ScheduledClassMetaData))]
    public partial class ScheduledClass
    {
        [Display(Name = "Class Summary")]
        public string ClassSummary
        {
            get { return StartDate + " | " + Cours.CourseName + " | " + Location; }
        }
    }

    #endregion

    #region ScheduledClassStatus Metadata

    public class ScheduledClassStatusMetaData
    {
        //public int SCSID { get; set; }
        [Required(ErrorMessage = "* Required")]
        [StringLength(50, ErrorMessage = "* Must be 50 characters or less.")]
        [Display(Name = "Course Status")]
        public string SCSName { get; set; }
    }
    [MetadataType(typeof(ScheduledClassStatusMetaData))]
    public partial class ScheduledClassStatus { }
    #endregion

    #region Student Metadata

    public class StudentMetaData
    {
        [Display(Name = "Student ID")]
        public int StudentID { get; set; }

        [Required(ErrorMessage = "* Required")]
        [StringLength(20, ErrorMessage = "* Must be 20 characters or less.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "* Required")]
        [StringLength(20, ErrorMessage = "* Must be 20 characters or less.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Major")]
        [StringLength(15, ErrorMessage = "* Must be 15 characters or less.")]
        public string Major { get; set; }

        [Display(Name = "Address")]
        [StringLength(50, ErrorMessage = "* Must be 50 characters or less.")]
        public string Address { get; set; }

        [Display(Name = "City")]
        [StringLength(25, ErrorMessage = "* Must be 25 characters or less.")]
        public string City { get; set; }

        [Display(Name = "State")]
        [StringLength(2, ErrorMessage = "* Must be 2 characters or less.")]
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        [StringLength(10, ErrorMessage = "* Must be 10 characters or less.")]
        public string ZipCode { get; set; }

        [Display(Name = "Phone Number")]
        [StringLength(13, ErrorMessage = "* Must be 13 characters or less.")]
        [DisplayFormat(DataFormatString = "{0:p}", ApplyFormatInEditMode = true)]
        [Phone]
        public string Phone { get; set; }

        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string Email { get; set; }

        public string PhotoUrl { get; set; }

        public int SSID { get; set; }
    }
    [MetadataType(typeof(StudentMetaData))]
    public partial class Student
    {
        [Display(Name = "Student Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName;  }
        }
    }
    #endregion

    #region StudentStatus Metadata

    public class StudentStatusMetaData
    {
        //public int SSID { get; set; }
        [Display(Name = "Status")]
        [StringLength(30, ErrorMessage = "* Must be 30 characters or less.")]
        public string SSName { get; set; }

        [Required(ErrorMessage = "* Required")]
        [StringLength(250, ErrorMessage = "* Must be 250 characters or less.")]
        [Display(Name = "Description")]
        public string SSDescription { get; set; }
    }
    [MetadataType(typeof(StudentStatusMetaData))]
    public partial class StudentStatus { }
    #endregion

}
