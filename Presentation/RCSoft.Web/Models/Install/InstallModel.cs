﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation.Attributes;
using RCSoft.Web.Validators.Install;
using RCSoft.Web.Framework.Mvc;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RCSoft.Web.Models.Install
{
    [Validator(typeof(InstallValidator))]
    public partial class InstallModel:BaseModel
    {
        public InstallModel()
        { 
        }
        [AllowHtml]
        public string AdminEmail { get; set; }
        [AllowHtml]
        [DataType(DataType.Password)]
        public string AdminPassword { get; set; }
        [AllowHtml]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [AllowHtml]
        public string DatabaseConnectionString { get; set; }
        public string DataProvider { get; set; }
        //SQL Server properties
        public string SqlConnectionInfo { get; set; }
        [AllowHtml]
        public string SqlServerName { get; set; }
        [AllowHtml]
        public string SqlDatabaseName { get; set; }
        [AllowHtml]
        public string SqlServerUsername { get; set; }
        [AllowHtml]
        public string SqlServerPassword { get; set; }
        public string SqlAuthenticationType { get; set; }
        public bool SqlServerCreateDatabase { get; set; }

        public bool UseCustomCollation { get; set; }
        [AllowHtml]
        public string Collation { get; set; }


        public bool InstallSampleData { get; set; }
    }
}