namespace Hexagonal.Common.Configurations
{
    public class AppSettings
    {
        /// <summary>
        /// Main DB connection String
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Minutes to expiration of Verification
        /// </summary>
        public int VerificationTimeout { get; set; }

        /// <summary>
        /// If true, it will accept any verification code, but it needs to be created first
        /// </summary>
        public bool VerificationMock { get; set; }
    }
}
