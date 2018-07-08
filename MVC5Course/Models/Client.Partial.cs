namespace MVC5Course.Models
{
    using MVC5Course.Models.InputValidations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ClientMetaData))]
    public partial class Client : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateOfBirth.HasValue && City != "")
            {
                if (DateOfBirth.Value.Year > 1980 && City == "Taipei")
                {
                    yield return new ValidationResult("條件錯誤", new string[] { "DateOfBirth", "City" });
                }
            }

            //若有些模型驗證是要在新增 或是 修改單方面才驗證時可以利用以下小技巧
            if (ClientId == 0)
            {
                //通常 P.K. 為 0 代表著正在執行「新增」動作
            }

            if (Longitude.HasValue)
            {
                if (Latitude.HasValue)
                {
                    yield return new ValidationResult("Longitude 與 Latitude 欄位都要一起設定或一起不設定");
                }
            }
        }
    }

    public partial class ClientMetaData
    {
        [Required]
        public int ClientId { get; set; }
        
        [StringLength(40, ErrorMessage="欄位長度不得大於 40 個字元")]
        [Required]
        public string FirstName { get; set; }
        
        [StringLength(40, ErrorMessage="欄位長度不得大於 40 個字元")]
        [Required]
        [NeedThree]
        public string MiddleName { get; set; }
        
        [StringLength(40, ErrorMessage="欄位長度不得大於 40 個字元")]
        [Required]
        public string LastName { get; set; }
        
        [StringLength(1, ErrorMessage="欄位長度不得大於 1 個字元")]
        [Required]
        public string Gender { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)] //此為改 Model 的做法(全站此欄位都統一時在此修改)
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<double> CreditRating { get; set; }
        
        [StringLength(7, ErrorMessage="欄位長度不得大於 7 個字元")]
        public string XCode { get; set; }
        public Nullable<int> OccupationId { get; set; }
        
        [StringLength(20, ErrorMessage="欄位長度不得大於 20 個字元")]
        public string TelephoneNumber { get; set; }
        
        [StringLength(100, ErrorMessage="欄位長度不得大於 100 個字元")]
        public string Street1 { get; set; }
        
        [StringLength(100, ErrorMessage="欄位長度不得大於 100 個字元")]
        public string Street2 { get; set; }
        
        [StringLength(100, ErrorMessage="欄位長度不得大於 100 個字元")]
        public string City { get; set; }
        
        [StringLength(15, ErrorMessage="欄位長度不得大於 15 個字元")]
        public string ZipCode { get; set; }
       
        public Nullable<double> Longitude { get; set; }

        public Nullable<double> Latitude { get; set; }
        public string Notes { get; set; }

        [IdCard]
        public string IdNumber { get; set; }

        public Nullable<bool> IsDelete { get; set; }

        //public string IsDeleteName {
        //    get {
        //        return IsDelete == true ? "已刪除" : "未刪除";
        //    }
        //}


        public virtual Occupation Occupation { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
