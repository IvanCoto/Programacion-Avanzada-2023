﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Domain.ConfigurationModels
{
    public class GoogleRecaptchaSettings
    {
        public string SiteKey { get; set; }

        public string SecretKey { get; set; }

        public string VerifyUrl { get; set; }
    }
}
