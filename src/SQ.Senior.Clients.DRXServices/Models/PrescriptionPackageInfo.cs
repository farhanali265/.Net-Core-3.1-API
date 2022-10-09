namespace SQ.Senior.Clients.DrxServices.Models {
    public class PrescriptionPackageInfo {
        public double CommonMetricQuantity { get; set; }
        public double CommonUserQuantity { get; set; }
        public bool IsCommonPackage { get; set; }
        public string PackageDescription { get; set; }
        public int PackageQuantity { get; set; }
        public double PackageSize { get; set; }
        public string PackageSizeUnitOfMeasure { get; set; }
        public string ReferenceNDC { get; set; }
        public string PackageDescriptionFull {
            get {
                // default description
                var pkgDesc = $"{PackageSize} {PackageSizeUnitOfMeasure}  {PackageDescription}";
                // if package of more then one
                if (PackageQuantity > 1)
                    pkgDesc = $"{pkgDesc} (Package of {PackageQuantity} {PackageDescription}";
                return pkgDesc;
            }
        }
        public float TotalPackageQuantity { get; set; }
    }
}
