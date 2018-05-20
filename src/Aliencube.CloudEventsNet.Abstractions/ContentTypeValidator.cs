using System;

namespace Aliencube.CloudEventsNet.Abstractions
{
    /// <summary>
    /// This represents the validator entity for content type of CloudEvent.
    /// </summary>
    public static class ContentTypeValidator
    {
        /// <summary>
        /// Checks whether the given type reference is <see cref="String"/> or not.
        /// </summary>
        /// <param name="type">Type of data.</param>
        /// <returns>Returns <c>True</c>, if the type reference is <see cref="String"/>; otherwise returns <c>False</c>.</returns>
        public static bool IsTypeString(Type type)
        {
            var result = type == typeof(string);

            return result;
        }

        /// <summary>
        /// Checks whether the given type reference is <see cref="Byte[]"/> or not.
        /// </summary>
        /// <param name="type">Type of data.</param>
        /// <returns>Returns <c>True</c>, if the type reference is <see cref="Byte[]"/>; otherwise returns <c>False</c>.</returns>
        public static bool IsTypeByteArray(Type type)
        {
            var result = type == typeof(byte[]);

            return result;
        }

        /// <summary>
        /// Checks whether the given data type is <see cref="String"/> or not.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="data">Data instance.</param>
        /// <returns>Returns <c>True</c>, if the given data type is <see cref="String"/>; otherwise returns <c>False</c>.</returns>
        public static bool IsDataString<T>(T data)
        {
            var result = data.GetType() == typeof(string);

            return result;
        }

        /// <summary>
        /// Checks whether the given data type is <see cref="Byte[]"/> or not.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="data">Data instance.</param>
        /// <returns>Returns <c>True</c>, if the given data type is <see cref="Byte[]"/>; otherwise returns <c>False</c>.</returns>
        public static bool IsDataByteArray<T>(T data)
        {
            var result = data.GetType() == typeof(byte[]);

            return result;
        }

        /// <summary>
        /// Checks whether the content type indicates JSON or not.
        /// </summary>
        /// <param name="contentType">Content type.</param>
        /// <returns>Returns <c>True</c>, if the content type indicates JSON; otherwise returns <c>False</c>.</returns>
        public static bool IsJson(string contentType)
        {
            var result = contentType.StartsWith("application/json", StringComparison.CurrentCultureIgnoreCase);

            return result;
        }

        /// <summary>
        /// Checks whether the content type contains JSON suffix or not.
        /// </summary>
        /// <param name="contentType">Content type.</param>
        /// <returns>Returns <c>True</c>, if the content type contains JSON suffix; otherwise returns <c>False</c>.</returns>
        public static bool HasJsonSuffix(string contentType)
        {
            var result = contentType.ToLowerInvariant().Contains("+json");

            return result;
        }

        /// <summary>
        /// Checks whether the content type implies JSON or not.
        /// </summary>
        /// <param name="contentType">Content type.</param>
        /// <returns>Returns <c>True</c>, if the content type implies JSON; otherwise returns <c>False</c>.</returns>
        public static bool ImpliesJson(string contentType)
        {
            var result = contentType.StartsWith("text/json", StringComparison.CurrentCultureIgnoreCase);

            return result;
        }

        /// <summary>
        /// Checks whether the content type indicates text or not.
        /// </summary>
        /// <param name="contentType">Content type.</param>
        /// <returns>Returns <c>True</c>, if the content type indicates text; otherwise returns <c>False</c>.</returns>
        public static bool IsText(string contentType)
        {
            var result = contentType.StartsWith("text/", StringComparison.CurrentCultureIgnoreCase);

            return result;
        }
    }
}