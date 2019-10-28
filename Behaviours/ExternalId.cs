using System;
using System.Diagnostics.CodeAnalysis;

namespace Behaviours
{
    public readonly struct ExternalId : IEquatable<ExternalId>
    {
        private static Random rng = new Random();

        // We only use the lowest 3 bytes of each int, always mask off 
        // the highest byte
        private const int Mask = 0xffffff;

        private const char SPECIAL_A = '_';
        private const char SPECIAL_B = '~';

        private static readonly char[] CharacterLookup = {
            '0', '1', '2', '3', '4', '5', '6', '7',
            '8', '9', 'a', 'b', 'c', 'd', 'e', 'f',
            'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
            'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
            'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D',
            'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L',
            'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
            'U', 'V', 'W', 'X', 'Y', 'Z',
            SPECIAL_A, SPECIAL_B,
        };

        private readonly int a;
        private readonly int b;
        private readonly int c;

        public static readonly ExternalId Empty = new ExternalId();

        public static Random Random
        {
            get => rng;
            set => rng = value ?? throw new ArgumentNullException(nameof(Random));
        }

        internal ExternalId(int a, int b, int c)
        {
            this.a = a & Mask;
            this.b = b & Mask;
            this.c = c & Mask;
        }

        public static ExternalId Create() => new ExternalId(Random.Next(), Random.Next(), Random.Next());

        public static ExternalId Parse(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (input.Length != 12) throw new ArgumentException($"Expecting {nameof(input)} to be 12 characters long");

            var a = GetCharIndex(input[0]);
            a |= GetCharIndex(input[1]) << 6;
            a |= GetCharIndex(input[2]) << 12;
            a |= GetCharIndex(input[3]) << 18;

            var b = GetCharIndex(input[4]);
            b |= GetCharIndex(input[5]) << 6;
            b |= GetCharIndex(input[6]) << 12;
            b |= GetCharIndex(input[7]) << 18;

            var c = GetCharIndex(input[8]);
            c |= GetCharIndex(input[9]) << 6;
            c |= GetCharIndex(input[10]) << 12;
            c |= GetCharIndex(input[11]) << 18;

            return new ExternalId(a, b, c);
        }

        public static bool TryParse(string input, out ExternalId externalId)
        {
            try
            {
                externalId = Parse(input);
                return true;
            }
            catch
            {
                externalId = Empty;
                return false;
            }
        }

        public override string ToString()
        {
            var result = new char[12];
            result[0] = CharacterLookup[a & 0x3f];
            result[1] = CharacterLookup[a >> 6 & 0x3f];
            result[2] = CharacterLookup[a >> 12 & 0x3f];
            result[3] = CharacterLookup[a >> 18 & 0x3f];

            result[4] = CharacterLookup[b & 0x3f];
            result[5] = CharacterLookup[b >> 6 & 0x3f];
            result[6] = CharacterLookup[b >> 12 & 0x3f];
            result[7] = CharacterLookup[b >> 18 & 0x3f];

            result[8] = CharacterLookup[c & 0x3f];
            result[9] = CharacterLookup[c >> 6 & 0x3f];
            result[10] = CharacterLookup[c >> 12 & 0x3f];
            result[11] = CharacterLookup[c >> 18 & 0x3f];
            return new string(result);
        }

        public static implicit operator string(ExternalId id) => id.ToString();

        private static int GetCharIndex(char character)
        {
            if (character >= '0' && character <= '9')
            {
                return character - '0';
            }
            else if (character >= 'a' && character <= 'z')
            {
                return character - 87;
            }
            else if (character >= 'A' && character <= 'Z')
            {
                return character - 29;
            }
            else if (character == SPECIAL_A)
            {
                return 62;
            }
            else if (character == SPECIAL_B)
            {
                return 63;
            }

            throw new ArgumentException("Unexpected character: " + character);
        }

        public override bool Equals(object obj) => obj is ExternalId id && Equals(id);

        public bool Equals([AllowNull] ExternalId other) => a == other.a && b == other.b && c == other.c;

        public override int GetHashCode() => HashCode.Combine(a, b, c);

        public static bool operator ==(ExternalId left, ExternalId right) => left.Equals(right);

        public static bool operator !=(ExternalId left, ExternalId right) => !left.Equals(right);
    }
}
