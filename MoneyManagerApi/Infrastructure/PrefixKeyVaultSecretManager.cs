using Microsoft.Azure.KeyVault.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace ExpenseManagerApi.Infrastructure
{
    public class PrefixKeyVaultSecretManager : IKeyVaultSecretManager
    {
        private readonly string prefix;
        private const string DelimiterPrefixAndKey = "-"; 
        private const string DelimiterKeyAndValue = "--"; 

        public PrefixKeyVaultSecretManager(string prefix)
        {
            this.prefix = prefix + DelimiterPrefixAndKey;
        }

        public string GetKey(SecretBundle secret)
        {
            return secret.SecretIdentifier.Name.Substring(prefix.Length)
                .Replace(DelimiterKeyAndValue, ConfigurationPath.KeyDelimiter);
        }

        public bool Load(SecretItem secret)
        {
            return secret.Identifier.Name.StartsWith(prefix);
        }
    }
}
