namespace NX.DependencyInjection
{
    /// <summary>
    /// 自動登録するコンポーネントのライフサイクルスコープ。
    /// </summary>
    public enum ComponentScope
    {
        /// <summary>
        /// シングルトン＝アプリケーションスコープ
        /// </summary>
        Singleton,

        /// <summary>
        /// スコープあり＝何らかの制限されたスコープ（主にはリクエスト）
        /// </summary>
        Scoped,

        /// <summary>
        /// スコープ無し
        /// </summary>
        Transient
    }
}
