using System;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;

using Fornax.Net.Util.Text;

namespace Fornax.Net.Util.Security.Cryptography
{
    /// <summary>
    /// Implements a 32-bit CRC(Cyclic Redundancy Check) algorithm compatible with Zip e.t.c.
    /// </summary>
    /// <remarks>
    /// Crc32 Should only be used for backward compatibility with older file formats and algorithms.
    /// It is not secue enough for new applications. 
    /// If you need to call multiple times for the same data either use the HashAlgorithm interface or 
    /// remeber that the result of one Compute call needs to be ~(XOR'ed) before being passed in as the seed for the next Compute call.
    /// </remarks>
    /// <seealso cref="Fornax.Net.Util.Security.Cryptography.IChecksum" />
    public sealed class CRC32 : HashAlgorithm, IChecksum
    {
        private UInt32 _crc = 0;
        private static readonly int SIZE = 256;
        private readonly UInt32 seed;
        private readonly UInt32[] table = new uint[SIZE];
        private UInt32 hash;

        /// <summary>
        /// The default polymonial
        /// </summary>
        public const UInt32 DefaultPolymonial = 0xedb88320u;
        /// <summary>
        /// The default seed
        /// </summary>
        public const UInt32 DefaultSeed = 0xffffffffu;


        private static readonly UInt32[] _crcTable = new uint[SIZE];

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public long Value => this._crc & DefaultSeed;

        /// <summary>
        /// Gets the size, in bits, of the computed hash code.
        /// </summary>
        public override int HashSize => 32;

        /// <summary>
        /// Resets this Algorithm
        /// </summary>
        public void Reset() {
            _crc = 0;
        }

        /// <summary>
        /// Updates the specified buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        public void Update(int buffer) {
            UInt32 c = ~_crc;
            c = this.table[(c ^ buffer) & 0xff] ^ (c >> 8);
            _crc = ~c;
        }

        /// <summary>
        /// Updates the specified buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        public void Update(byte[] buffer) {
            Update(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Updates the specified buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        public void Update(byte[] buffer, int offset, int length) {
            UInt32 c = ~_crc;
            while (--length >= 0)
                c = this.table[(c ^ buffer[offset++]) & 0xff] ^ (c >> 8);
            _crc = c;
        }

        /// <summary>
        /// Initializes an implementation of the <see cref="T:System.Security.Cryptography.HashAlgorithm" /> class.
        /// </summary>
        public override void Initialize() => this.hash = seed;

        /// <summary>
        /// Computes the CRC32-Hash for the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static uint Compute(string text) {
            Contract.Requires(!text.IsEmptyorNull());
            return Compute(System.Text.Encoding.Default.GetBytes(text));
        }

        /// <summary>
        /// Computes the Hash Code for the specified buffer.
        /// <see cref="DefaultPolymonial"/> and <see cref="DefaultSeed"/> are used for computation.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <returns></returns>
        public static uint Compute(byte[] buffer) {
            return Compute(DefaultSeed, buffer);
        }

        /// <summary>
        /// Computes the Hash code with the specified seed.
        /// <see cref="DefaultPolymonial"/> is use as polynomial for computation.
        /// </summary>
        /// <param name="seed">The seed.</param>
        /// <param name="buffer">The buffer.</param>
        /// <returns></returns>
        public static uint Compute(uint seed, byte[] buffer) {
            return Compute(DefaultPolymonial, seed, buffer);
        }

        /// <summary>
        /// Computes the Hash code with the specified polynomial.
        /// </summary>
        /// <param name="polynomial">The polynomial.</param>
        /// <param name="seed">The seed.</param>
        /// <param name="buffer">The buffer.</param>
        /// <returns></returns>
        public static uint Compute(uint polynomial, uint seed, byte[] buffer) {
            uint[] tempTable = new uint[SIZE];
                InitTable(polynomial, ref tempTable);
            return ~CalculateHash(tempTable, seed, buffer, 0, buffer.Length);
        }


        protected override void HashCore(byte[] array, int ibStart, int cbSize) {
            this.hash = CalculateHash(table, hash, array, ibStart, cbSize);
        }

        private static uint CalculateHash(uint[] table, uint seed, byte[] buffer, int ibStart, int cbSize) {
            var h = seed;
            for (var i = ibStart; i < ibStart + cbSize; i++) {
                seed = (seed >> 8) ^ table[buffer[i] ^ seed & 0xff];
            }
            return seed;
        }

        protected override byte[] HashFinal() {
            var hashBuf = UInt32ToBigEndian(~hash);
            HashValue = hashBuf;
            return hashBuf;
        }

        /// <summary>
        ///  Converts Unsigned integer to a byte array in Big-Endian Format.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <returns>The Big-Endian [<seealso cref="System.Text.Encoding.BigEndianUnicode"/>]
        /// byte array representation of the specified unsigned integer <paramref name="v"/>.</returns>
        private static byte[] UInt32ToBigEndian(uint v) {
            var result = BitConverter.GetBytes(v);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(result);
            return result;
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this CRC32 hash code.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() {
            return base.ToString();
        }

        static CRC32() {
            InitTable(DefaultPolymonial, ref _crcTable);
        }

        /// <summary>
        /// Initializes the CRC32 Table using polynomial input.
        /// </summary>
        /// <param name="polynomial">The polynomial.</param>
        /// <param name="table">The table.</param>
        private static void InitTable(uint polynomial, ref uint[] table) {
            if (polynomial == DefaultPolymonial) table = _crcTable;
            else
                for (uint i = 0; i < table.Length; i++) {
                    uint entry = i;
                    for (var j = 8; --j >= 0;) {
                        if ((entry & 1) != 0)
                            entry = polynomial ^ (entry >> 1);
                        else
                            entry = entry >> 1;
                    }
                    table[i] = entry;
                }
        }

        /// <summary>     
        /// This Constructor uses the Default Polynomial <see cref="DefaultPolymonial"/>,
        /// and Default Seed <see cref="DefaultSeed"/>  to create CRC32 table.
        /// Initializes a new instance of the <see cref="CRC32" /> class.
        /// </summary>
        public CRC32() : this(DefaultPolymonial, DefaultSeed) { }

        /// <summary> This Constructor uses Custom input polynomial and seed values to create CRC32 table.
        /// Initializes a new instance of the <see cref="CRC32" /> class.
        /// </summary>
        /// <param name="polynomial">The polynomial.</param>
        /// <param name="seed">The seed.</param>
        public CRC32(uint polynomial, uint seed) {
            InitTable(polynomial, ref table);
        }
    }
}
