using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json.Linq;
using SQ.Senior.SpecialEnrollmentPeriods.Marx.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SQ.Senior.SpecialEnrollmentPeriods.Marx.DataStructure {
    public static class MedicareEnrollmentInformationDictionary {
        
        private static Dictionary<string, PlanType> MedicareEnrollmentInformationData;
        
        public static PlanType GetValueByKey(string key) {

            if(MedicareEnrollmentInformationData is null) {
                LoadData();
            }

            if (!MedicareEnrollmentInformationData.ContainsKey(key)) {
                return default(PlanType);
            }
            return MedicareEnrollmentInformationData[key];
        }

        private static void LoadData() {
            MedicareEnrollmentInformationData = new Dictionary<string, PlanType>() {
                { "01 - HMO", PlanType.MA},
                {"02 - HMOPOS", PlanType.MA},
                { "04 - LOCAL PPO", PlanType.MA},
                {"09 - PPFS", PlanType.MA},
                { "18 - 1876 COST", PlanType.OTHER},
                {"20 - NATIONAL PACE", PlanType.OTHER},
                { "29 - MEDICARE PRESCRIPTION DRUG PLAN", PlanType.PDP},
                {"31 - REGIONAL PPO", PlanType.MA},
                { "48 - MEDICARE-MADICAID PLAN HMO", PlanType.MA},
                {"07 - MSA", PlanType.OTHER},
                { "30 - EMPLOYER/UNION ONLY DIRECT CONTRACT PDP", PlanType.PDP},
                {"46 - POINT-OF-SALE CONTRACTOR", PlanType.PDP}
            };
        }
    }
}
