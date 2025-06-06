using DotNetEnv;

namespace userJwtApp.Dotenv;

public static class Dotenv
{

    public static readonly string JWT_SIMETRIC_KEY_SHA256 =
        Environment.GetEnvironmentVariable("JWT_SYMETRIC_KEY") ?? throw new Exception(" JWT Key NOT set!");
    public static readonly string CLAIM_ID =
        Environment.GetEnvironmentVariable("CLAIM_ID") ?? throw new Exception(" CLAIM ID NOT set!");
    public static readonly string JWT_ISSUER =
        Environment.GetEnvironmentVariable("JWT_ISSUER") ?? throw new Exception(" JWT ISSUER NOT set!");

}