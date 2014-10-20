using System;
using System.Globalization;
using System.Text;
using Pulse.Core;

namespace Pulse.Text
{
    public sealed class FFXIIITextTag
    {
        public const int MaxTagLength = 32;

        public readonly FFXIIITextTagCode Code;
        public readonly Enum Param;

        public FFXIIITextTag(FFXIIITextTagCode code, Enum param = null)
        {
            Code = code;
            Param = param;
        }

        public int Write(byte[] bytes, ref int offset)
        {
            bytes[offset++] = (byte)Code;
            if (Param == null)
                return 1;

            bytes[offset++] = (byte)(FFXIIITextTagParam)Param;
            return 2;
        }

        public int Write(char[] chars, ref int offset)
        {
            StringBuilder sb = new StringBuilder(MaxTagLength);
            sb.Append('{');
            sb.Append(Code);
            if (Param != null)
            {
                sb.Append(' ');
                sb.Append(Param);
            }
            sb.Append('}');

            if (sb.Length > MaxTagLength)
                throw Exceptions.CreateException("������� ������� ��� ����: {0}", sb.ToString());

            for (int i = 0; i < sb.Length; i++)
                chars[offset++] = sb[i];

            return sb.Length;
        }

        public static FFXIIITextTag TryRead(byte[] bytes, ref int offset, ref int left)
        {
            FFXIIITextTagCode code = (FFXIIITextTagCode)bytes[offset++];
            left -= 2;
            switch (code)
            {
                case FFXIIITextTagCode.End:
                case FFXIIITextTagCode.Question:
                case FFXIIITextTagCode.Italic:
                case FFXIIITextTagCode.Many:
                case FFXIIITextTagCode.Article:
                case FFXIIITextTagCode.ArticleMany:
                    left++;
                    return new FFXIIITextTag(code);
                case FFXIIITextTagCode.Icon:
                case FFXIIITextTagCode.Var:
                    return new FFXIIITextTag(code, (FFXIIITextTagParam)bytes[offset++]);
                case FFXIIITextTagCode.Text:
                    return new FFXIIITextTag(code, (FFXIIITextTagText)bytes[offset++]);
                case FFXIIITextTagCode.Key:
                    return new FFXIIITextTag(code, (FFXIIITextTagKey)bytes[offset++]);
                default:
                    left += 2;
                    offset--;
                    return null;
            }
        }

        public static FFXIIITextTag TryRead(char[] chars, ref int offset, ref int left)
        {
            int oldOffset = offset;
            int oldleft = left;

            string tag, par;
            if (chars[offset++] != '{' || !TryGetTag(chars, ref offset, ref left, out tag, out par))
            {
                offset = oldOffset;
                left = oldleft;
                return null;
            }

            FFXIIITextTagCode? code = EnumCache<FFXIIITextTagCode>.TryParse(tag);
            if (code == null)
            {
                offset = oldOffset;
                left = oldleft;
                return null;
            }

            switch (code.Value)
            {
                case FFXIIITextTagCode.End:
                case FFXIIITextTagCode.Question:
                case FFXIIITextTagCode.Italic:
                case FFXIIITextTagCode.Many:
                case FFXIIITextTagCode.Article:
                case FFXIIITextTagCode.ArticleMany:
                    return new FFXIIITextTag(code.Value);
                case FFXIIITextTagCode.Var:
                case FFXIIITextTagCode.Icon:
                {
                    byte numArg;
                    if (byte.TryParse(par, NumberStyles.Integer, CultureInfo.InvariantCulture, out numArg))
                        return new FFXIIITextTag(code.Value, (FFXIIITextTagParam)numArg);
                    break;
                }
                case FFXIIITextTagCode.Text:
                {
                    FFXIIITextTagText? arg = EnumCache<FFXIIITextTagText>.TryParse(par);
                    if (arg != null) return new FFXIIITextTag(code.Value, arg.Value);
                    break;
                }
                case FFXIIITextTagCode.Key:
                {
                    FFXIIITextTagKey? arg = EnumCache<FFXIIITextTagKey>.TryParse(par);
                    if (arg != null) return new FFXIIITextTag(code.Value, arg.Value);
                    break;
                }
            }

            offset = oldOffset;
            left = oldleft;
            return null;
        }

        private static bool TryGetTag(char[] chars, ref int offset, ref int left, out string tag, out string par)
        {
            int lastIndex = Array.IndexOf(chars, '}', offset);
            int length = lastIndex - offset + 1;
            if (length < 2)
            {
                tag = null;
                par = null;
                return false;
            }

            left--;
            left -= length;

            int spaceIndex = Array.IndexOf(chars, ' ', offset + 1, length - 2);
            if (spaceIndex < 0)
            {
                tag = new string(chars, offset, length - 1);
                par = string.Empty;
            }
            else
            {
                tag = new string(chars, offset, spaceIndex - offset);
                par = new string(chars, spaceIndex + 1, lastIndex - spaceIndex - 1);
            }

            offset = lastIndex + 1;
            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(MaxTagLength);
            sb.Append('{');
            sb.Append(Code);
            if (Param != null)
            {
                sb.Append(' ');
                sb.Append(Param);
            }
            sb.Append('}');
            return sb.ToString();
        }
    }
}