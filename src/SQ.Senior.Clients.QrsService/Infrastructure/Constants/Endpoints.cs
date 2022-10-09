namespace SQ.Senior.Clients.QrsService.Infrastructure.Constants {
    public static class Endpoints {
        public const string Applicants = "applicants";
        public const string Interactions = "interactions";
        public const string ContactTypes = "contact-types";
        public const string Contact = "interactions/{0}/contacts";
        public const string Provider = "interactions/{0}/providers";
        public const string Pharmacy = "interactions/{0}/pharmacies";
        public const string Prescription = "interactions/{0}/prescriptions";
        public const string Plans = "interactions/{0}/plans";
        public const string EffectivePlan = "applicants/{0}/plans/{1}/effective";
        public static string ApplicantConsolidate = "applicants/:id/consolidate";
        public const string ApplicantPlan = "applicants-plans";
        public const string ApplicantProvider = "applicants-providers";
        public const string ApplicantPharmacy = "applicants-pharmacies";
        public const string ApplicantPrescription = "applicants-prescriptions";
        public const string ApplicantContact = "applicants-contacts";
        public static string ApplicantToken = "applicants-tokens/authenticate";
        public static string ProviderSearch = "providers/search?page=[PAGE]&limit=[LIMIT]&strict=false";
        public static string APIKeyName = "x-api-key";
        public static string Page = "[PAGE]";
        public static string Limit = "[LIMIT]";
    }
}