namespace TombTexPacker
{
    public static class StringExtension
    {
        /// <summary>
        /// Get the path (without filename and extension) and filename without extension
        /// </summary>
        /// <param name="destPath">A path</param>
        /// <returns>First is the path without filename and extension, Second is the filename with extension</returns>
        public static (string, string) GetSeparatedPathAndFilename(this string value)
        {
            string path = Path.GetFullPath(value);
            string fileName = Path.GetFileName(path);
            return (path.Replace(fileName, string.Empty), fileName);
        }
    }
}
