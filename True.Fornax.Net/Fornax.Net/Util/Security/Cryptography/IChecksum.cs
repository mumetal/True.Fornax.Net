using Algorithm = System.Security.Cryptography.HashAlgorithm;
/// <summary>
/// <see cref="Fornax.Net.Util.Security.Cryptography"/>
/// </summary>
namespace Fornax.Net.Util.Security.Cryptography
{
    /// <summary>
    /// Interface represents a Hash-Checksum validator, and defines the methods for manipulating 
    /// hash codes.
    /// </summary>
    public interface IChecksum 
    {
        void Reset();

        void Update(int buffer);

        void Update(byte[] buffer);

        void Update(byte[] buffer, int offset, int length);

        long Value { get; }
    }
}
