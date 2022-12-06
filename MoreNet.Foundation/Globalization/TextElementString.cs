﻿using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MoreNet.Foundation.Globalization
{
    // TODO: exception design
    // TODO: testing, expected as string instead of self-designed result if appropriate
    //       ex: actual.Should().Be("".Contains(""));
    /// <summary>
    /// Wrapper for <see cref="TextElementEnumerator"/> and provide commonly used methods for problems about grapheme.
    /// Some of methods from <see cref="string"/> handle grapheme problems, we do not provide those methods.
    /// </summary>
    public class TextElementString : IEnumerable<string>
    {
        private List<string> _underlyingTextElements;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextElementString"/> class.
        /// </summary>
        /// <param name="value">The string to seek.</param>
        public TextElementString(string value)
        {
            _underlyingTextElements = GenerateTextElements(value);
        }

        private TextElementString(List<string> value)
        {
            _underlyingTextElements = value;
        }

        /// <summary>
        /// Gets the <see cref="string"/> object at a specified position in the current <see cref="TextElementString"/> object.
        /// </summary>
        /// <param name="index">A position in the current <see cref="TextElementString"/>.</param>
        /// <returns>The object at position index.</returns>
        public string this[int index] => _underlyingTextElements[index];

        /// <summary>
        /// Defines an implicit conversion of a <see cref="TextElementString"/> to a <see cref="string"/>.
        /// </summary>
        /// <param name="value">The <see cref="TextElementString"/> to convert.</param>
        public static implicit operator string(TextElementString value)
        {
            return value.ToString();
        }

        /// <summary>
        /// Returns a value indicating whether a specified substring occurs within this text element.
        /// </summary>
        /// <param name="value">The string to seek.</param>
        /// <returns>Contains.</returns>
        public bool Contains(string value)
        {
            Argument.ShouldNotNull(value, nameof(value));

            if (value.Length == 0)
            {
                return true;
            }

            foreach (var matching in KMPMatching(value))
            {
                if (matching.MatchFrom.HasValue)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns a new string in which all occurrences of a specified string in terms of text element
        /// in this instance are replaced with another specified string.
        /// </summary>
        /// <param name="oldValue">The string to be replaced.</param>
        /// <param name="newValue">The string to replace all occurrences of oldValue.</param>
        /// <returns>
        /// A string that is equivalent in terms of text element to the current string except that all instances of
        /// <paramref name="oldValue"/> replaced with <paramref name="newValue"/>.
        /// </returns>
        public TextElementString Replace(string oldValue, string newValue)
        {
            var newUnderlyingTextElements = new List<string>();
            int i = 0;
            foreach (var matching in KMPMatching(oldValue))
            {
                int matchFrom = matching.MatchFrom ?? _underlyingTextElements.Count;
                while (i < matchFrom)
                {
                    newUnderlyingTextElements.Add(_underlyingTextElements[i]);
                    i++;
                }

                if (matching.MatchFrom.HasValue)
                {
                    var newTextElements = GenerateTextElements(newValue);
                    newUnderlyingTextElements.AddRange(newTextElements);
                    i += matching.Count;
                }
            }

            _underlyingTextElements = newUnderlyingTextElements;
            return this;
        }

        /// <summary>
        /// Retrieves a subset of text element from this instance. The subset of text element starts at a specified
        /// text element position and continues to the end of the string.
        /// </summary>
        /// <param name="startIndex">The zero-based starting text element position of a subset of text element in this instance.</param>
        /// <returns>
        /// A new instance of <see cref="TextElementString"/> contains subset of text element,
        /// or empty if <paramref name="startIndex"/> is equal to the length of this instance.
        /// </returns>
        public TextElementString SubTextElementString(int startIndex)
        {
            return SubTextElementString(startIndex, _underlyingTextElements.Count - startIndex);
        }

        /// <summary>
        /// Retrieves a subset of text element from this instance. The subset of text element starts at a specified
        /// text element position and has a specified length.
        /// </summary>
        /// <param name="startIndex">The zero-based starting text element position of a subset of text element in this instance.</param>
        /// <param name="length">The number of text elements in the <see cref="TextElementString"/>.</param>
        /// <returns>
        /// A new instance of <see cref="TextElementString"/> contains subset of text element,
        /// or empty if <paramref name="startIndex"/> is equal to the length of this instance.
        /// </returns>
        public TextElementString SubTextElementString(int startIndex, int length)
        {
            var newTextElements = new List<string>();
            int i = startIndex - 1;
            while (++i < startIndex + length)
            {
                newTextElements.Add(_underlyingTextElements[i]);
            }

            return new TextElementString(newTextElements);
        }

        /// <summary>
        /// Removes the element after the specified index (included) of the <see cref="TextElementString"/>.
        /// </summary>
        /// <param name="startIndex">The zero-based index of the elements start to remove.</param>
        /// <returns>Current <see cref="TextElementString"/>.</returns>
        public TextElementString Remove(int startIndex)
        {
            var count = _underlyingTextElements.Count - startIndex;
            Remove(startIndex, count);
            return this;
        }

        /// <summary>
        /// Removes a range of elements from the <see cref="TextElementString"/>.
        /// </summary>
        /// <param name="startIndex">The zero-based starting index of the range of elements to remove.</param>
        /// <param name="count">The number of elements to remove.</param>
        /// <returns>Current <see cref="TextElementString"/>.</returns>
        public TextElementString Remove(int startIndex, int count)
        {
            _underlyingTextElements.RemoveRange(startIndex, count);
            return this;
        }

        /// <summary>
        /// Reverses the order of the elements in the entire <see cref="TextElementString"/>.
        /// </summary>
        /// <returns>Current <see cref="TextElementString"/>.</returns>
        public TextElementString Reverse()
        {
            _underlyingTextElements.Reverse();
            return this;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var textElement in _underlyingTextElements)
            {
                sb.Append(textElement);
            }

            return sb.ToString();
        }

        /// <inheritdoc/>
        public IEnumerator<string> GetEnumerator()
        {
            return _underlyingTextElements.GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _underlyingTextElements.GetEnumerator();
        }

        private List<string> GenerateTextElements(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return new List<string>();
            }

            var pattern = new List<string>();
            var patternEnumerator = StringInfo.GetTextElementEnumerator(value);
            while (patternEnumerator.MoveNext())
            {
                pattern.Add(patternEnumerator.GetTextElement());
            }

            return pattern;
        }

        private IEnumerable<(int? MatchFrom, int Count)> KMPMatching(string pattern)
        {
            var patternTextElements = GenerateTextElements(pattern);
            var next = GenerateNext(patternTextElements.ToArray());

            int i = 0;
            int j = 0;
            while (i < _underlyingTextElements.Count)
            {
                if (UnicodeNormalizeEquals(_underlyingTextElements[i], patternTextElements[j]))
                {
                    if (j == patternTextElements.Count - 1)
                    {
                        yield return (i - j, patternTextElements.Count);
                        j = 0;
                    }
                    else
                    {
                        j++;
                    }

                    i++;
                }
                else
                {
                    if (j > 0)
                    {
                        j = next[j - 1];
                    }
                    else
                    {
                        i++;
                    }
                }
            }

            yield return (null, default);

            int[] GenerateNext(string[] textElements)
            {
                var generated = new int[textElements.Length];
                generated[0] = 0;

                // prefixIndex also be perfixLength
                int prefixIndex = 0;
                int suffixIndex = 1;
                while (suffixIndex < textElements.Length)
                {
                    if (textElements[suffixIndex] == textElements[prefixIndex])
                    {
                        prefixIndex++;
                        generated[suffixIndex] = prefixIndex;
                        suffixIndex++;
                    }
                    else
                    {
                        if (prefixIndex == 0)
                        {
                            // Default of int is 0, no need to assign 0 to current item of int[] generated.
                            suffixIndex++;
                        }
                        else
                        {
                            prefixIndex = generated[prefixIndex - 1];
                        }
                    }
                }

                return generated;
            }
        }

        private bool UnicodeNormalizeEquals(string left, string right)
        {
            if (left == right)
            {
                return true;
            }

            return left.Normalize() == right.Normalize();
        }
    }
}
