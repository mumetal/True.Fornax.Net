using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;


namespace Fornax.Net.Util.Security.Cryptography
{

    /// <summary>
    /// Implements a 64-bit CRC hash algorithm for a given polynomial.
    /// </summary>
    /// <remarks>
    /// For ISO 3309 compliant 64-bit CRC's use Crc64Iso.
    /// </remarks>
    public class CRC64 : HashAlgorithm
    {
        public const UInt64 DefaultSeed = 0x0;

        readonly UInt64[] table;

        readonly UInt64 seed;
        UInt64 hash;

        /// <summary>
        /// Initializes a new instance of the <see cref="CRC64"/> class.
        /// </summary>
        /// <param name="polynomial">The polynomial.</param>
        public CRC64(UInt64 polynomial)
            :  this(polynomial, DefaultSeed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CRC64"/> class.
        /// </summary>
        /// <param name="polynomial">The polynomial.</param>
        /// <param name="seed">The seed.</param>
        public CRC64(UInt64 polynomial, UInt64 seed) {
            table = InitializeTable(polynomial);
            this.seed = hash = seed;
        }

        /// <summary>
        /// Initializes an implementation of the <see cref="T:System.Security.Cryptography.HashAlgorithm" /> class.
        /// </summary>
        public override void Initialize() {
            hash = seed;
        }

        protected override void HashCore(byte[] array, int ibStart, int cbSize) {
            hash = CalculateHash(hash, table, array, ibStart, cbSize);
        }

        protected override byte[] HashFinal() {
            var hashBuffer = UInt64ToBigEndianBytes(hash);
            HashValue = hashBuffer;
            return hashBuffer;
        }
        /// <summary>
        /// Gets the size, in bits, of the computed hash code.
        /// </summary>
        public override int HashSize { get { return 64; } }

        protected static UInt64 CalculateHash(UInt64 seed, UInt64[] table, IList<byte> buffer, int start, int size) {
            var hash = seed;
            for (var i = start; i < start + size; i++)
                unchecked {
                    hash = (hash >> 8) ^ table[(buffer[i] ^ hash) & 0xff];
                }
            return hash;
        }

        static byte[] UInt64ToBigEndianBytes(UInt64 value) {
            var result = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(result);

            return result;
        }

        static UInt64[] InitializeTable(UInt64 polynomial) {
            if (polynomial == Crc64Iso.Iso3309Polynomial && Crc64Iso.Table != null)
                return Crc64Iso.Table;

            var createTable = CreateTable(polynomial);

            if (polynomial == Crc64Iso.Iso3309Polynomial)
                Crc64Iso.Table = createTable;

            return createTable;
        }

        protected static ulong[] CreateTable(ulong polynomial) {
            var createTable = new UInt64[256];
            for (var i = 0; i < 256; ++i) {
                var entry = (UInt64)i;
                for (var j = 0; j < 8; ++j)
                    if ((entry & 1) == 1)
                        entry = (entry >> 1) ^ polynomial;
                    else
                        entry = entry >> 1;
                createTable[i] = entry;
            }
            return createTable;
        }
    }

    /// <summary>
    /// ISO 3309 compliant 64-bit CRC's.
    /// </summary>
    /// <seealso cref="Fornax.Net.Util.Security.Cryptography.CRC64" />
    public class Crc64Iso : CRC64
    {
        internal static UInt64[] Table;

        /// <summary>
        /// The iso3309 polynomial
        /// </summary>
        public const UInt64 Iso3309Polynomial = 0xD800000000000000;

        /// <summary>
        /// Initializes a new instance of the <see cref="Crc64Iso"/> class.
        /// </summary>
        public Crc64Iso()
            : base(Iso3309Polynomial) {
        }

        public Crc64Iso(UInt64 seed)
            : base(Iso3309Polynomial, seed) {
        }

        /// <summary>
        /// Computes the crc64 hash of the specified buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <returns></returns>
        public static UInt64 Compute(byte[] buffer) {
            return Compute(DefaultSeed, buffer);
        }

        /// <summary>
        /// Computes the CRC64 hash of the specified seed.
        /// </summary>
        /// <param name="seed">The seed.</param>
        /// <param name="buffer">The buffer.</param>
        /// <returns></returns>
        public static UInt64 Compute(UInt64 seed, byte[] buffer) {
            if (Table == null)
                Table = CreateTable(Iso3309Polynomial);

            return CalculateHash(seed, Table, buffer, 0, buffer.Length);
        }

    }
}
