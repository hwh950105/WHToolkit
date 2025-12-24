namespace WHToolkit.Database
{
    /// <summary>
    /// 배열 형태의 객체 값을 반환하는 인터페이스입니다.
    /// </summary>
    public interface IArrayObject
    {
        /// <summary>
        /// 객체의 값들을 배열 형태로 반환합니다.
        /// </summary>
        /// <returns>객체 값들의 배열</returns>
        object[] GetValueObject();
    }
}