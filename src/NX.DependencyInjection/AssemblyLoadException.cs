using System;

namespace NX.DependencyInjection
{
    /// <summary>
    /// アセンブリ読み込み時エラー
    /// </summary>
    [Serializable]
    public class AssemblyLoadException : Exception
    {
        public AssemblyLoadException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
