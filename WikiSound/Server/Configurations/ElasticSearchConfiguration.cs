namespace WikiSound.Server.Configurations
{
    internal class ElasticSearchConfiguration
    {
        public string Uri { get; set; }
        public string CertificateFingerprint { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
