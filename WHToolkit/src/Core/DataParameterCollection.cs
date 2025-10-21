using System.Collections;
using System.Data;

namespace WHToolkit.Database;

/// <summary>
/// DataParameter 객체의 컬렉션을 관리하는 클래스입니다.
/// </summary>
public class DataParameterCollection : ICollection<DataParameter>
{
    private readonly List<DataParameter> _parameters = new();

    /// <summary>
    /// 지정된 인덱스의 DataParameter를 가져오거나 설정합니다.
    /// </summary>
    /// <param name="index">인덱스</param>
    /// <returns>DataParameter 인스턴스</returns>
    public DataParameter this[int index]
    {
        get => _parameters[index];
        set => _parameters[index] = value;
    }

    /// <summary>
    /// 지정된 매개변수 이름의 DataParameter를 가져옵니다.
    /// </summary>
    /// <param name="parameterName">매개변수 이름</param>
    /// <returns>DataParameter 인스턴스 또는 null</returns>
    public DataParameter? this[string parameterName]
    {
        get => _parameters.FirstOrDefault(p => p.ParameterName == parameterName);
    }

    /// <summary>
    /// 컬렉션의 매개변수 개수
    /// </summary>
    public int Count => _parameters.Count;

    /// <summary>
    /// 컬렉션이 읽기 전용인지 여부
    /// </summary>
    public bool IsReadOnly => false;

    /// <summary>
    /// DataParameter를 컬렉션에 추가합니다.
    /// </summary>
    /// <param name="item">추가할 DataParameter</param>
    public void Add(DataParameter item)
    {
        _parameters.Add(item);
    }

    /// <summary>
    /// 매개변수 이름과 값으로 새 DataParameter를 생성하여 추가합니다.
    /// </summary>
    /// <param name="parameterName">매개변수 이름</param>
    /// <param name="value">매개변수 값</param>
    /// <returns>추가된 DataParameter</returns>
    public DataParameter Add(string parameterName, object? value)
    {
        var parameter = new DataParameter(parameterName, value);
        _parameters.Add(parameter);
        return parameter;
    }

    /// <summary>
    /// 매개변수 이름, 값, 크기로 새 DataParameter를 생성하여 추가합니다.
    /// </summary>
    /// <param name="parameterName">매개변수 이름</param>
    /// <param name="value">매개변수 값</param>
    /// <param name="size">매개변수 크기</param>
    /// <returns>추가된 DataParameter</returns>
    public DataParameter Add(string parameterName, object? value, int size)
    {
        var parameter = new DataParameter(parameterName, value, size);
        _parameters.Add(parameter);
        return parameter;
    }

    /// <summary>
    /// DbType, 매개변수 이름, 값으로 새 DataParameter를 생성하여 추가합니다.
    /// </summary>
    /// <param name="dbType">데이터베이스 타입</param>
    /// <param name="parameterName">매개변수 이름</param>
    /// <param name="value">매개변수 값</param>
    /// <returns>추가된 DataParameter</returns>
    public DataParameter Add(DbType dbType, string parameterName, object? value)
    {
        var parameter = new DataParameter(dbType, parameterName, value);
        _parameters.Add(parameter);
        return parameter;
    }

    /// <summary>
    /// 방향, 매개변수 이름, 값으로 새 DataParameter를 생성하여 추가합니다.
    /// </summary>
    /// <param name="direction">매개변수 방향</param>
    /// <param name="parameterName">매개변수 이름</param>
    /// <param name="value">매개변수 값</param>
    /// <returns>추가된 DataParameter</returns>
    public DataParameter Add(ParameterDirection direction, string parameterName, object? value)
    {
        var parameter = new DataParameter(direction, parameterName, value);
        _parameters.Add(parameter);
        return parameter;
    }

    /// <summary>
    /// 방향, 매개변수 이름, 값, 크기로 새 DataParameter를 생성하여 추가합니다.
    /// </summary>
    /// <param name="direction">매개변수 방향</param>
    /// <param name="parameterName">매개변수 이름</param>
    /// <param name="value">매개변수 값</param>
    /// <param name="size">매개변수 크기</param>
    /// <returns>추가된 DataParameter</returns>
    public DataParameter Add(ParameterDirection direction, string parameterName, object? value, int size)
    {
        var parameter = new DataParameter(direction, parameterName, value, size);
        _parameters.Add(parameter);
        return parameter;
    }

    /// <summary>
    /// 방향, DbType, 매개변수 이름, 값으로 새 DataParameter를 생성하여 추가합니다.
    /// </summary>
    /// <param name="direction">매개변수 방향</param>
    /// <param name="dbType">데이터베이스 타입</param>
    /// <param name="parameterName">매개변수 이름</param>
    /// <param name="value">매개변수 값</param>
    /// <returns>추가된 DataParameter</returns>
    public DataParameter Add(ParameterDirection direction, DbType dbType, string parameterName, object? value)
    {
        var parameter = new DataParameter(direction, dbType, parameterName, value);
        _parameters.Add(parameter);
        return parameter;
    }

    /// <summary>
    /// 여러 DataParameter를 컬렉션에 추가합니다.
    /// </summary>
    /// <param name="values">추가할 DataParameter 컬렉션</param>
    public void AddRange(IEnumerable<DataParameter> values)
    {
        foreach (var value in values)
        {
            _parameters.Add(value);
        }
    }

    /// <summary>
    /// 컬렉션의 모든 항목을 제거합니다.
    /// </summary>
    public void Clear()
    {
        _parameters.Clear();
    }

    /// <summary>
    /// 지정된 DataParameter가 컬렉션에 포함되어 있는지 확인합니다.
    /// </summary>
    /// <param name="item">확인할 DataParameter</param>
    /// <returns>포함 여부</returns>
    public bool Contains(DataParameter item)
    {
        return _parameters.Contains(item);
    }

    /// <summary>
    /// 지정된 매개변수 이름이 컬렉션에 포함되어 있는지 확인합니다.
    /// </summary>
    /// <param name="parameterName">확인할 매개변수 이름</param>
    /// <returns>포함 여부</returns>
    public bool Contains(string parameterName)
    {
        return _parameters.Any(p => string.Equals(p.ParameterName.Trim(), parameterName.Trim(), StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// 컬렉션의 요소를 배열로 복사합니다.
    /// </summary>
    /// <param name="array">대상 배열</param>
    /// <param name="arrayIndex">복사를 시작할 배열 인덱스</param>
    public void CopyTo(DataParameter[] array, int arrayIndex)
    {
        _parameters.CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// 지정된 DataParameter를 컬렉션에서 제거합니다.
    /// </summary>
    /// <param name="item">제거할 DataParameter</param>
    /// <returns>제거 성공 여부</returns>
    public bool Remove(DataParameter item)
    {
        return _parameters.Remove(item);
    }

    /// <summary>
    /// 컬렉션을 반복하는 열거자를 반환합니다.
    /// </summary>
    /// <returns>열거자</returns>
    public IEnumerator<DataParameter> GetEnumerator()
    {
        return _parameters.GetEnumerator();
    }

    /// <summary>
    /// 컬렉션을 반복하는 열거자를 반환합니다.
    /// </summary>
    /// <returns>열거자</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}