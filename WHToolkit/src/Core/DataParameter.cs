using System.Data;
using System.Data.Common;

namespace WHToolkit.Database;

/// <summary>
/// 다중 데이터베이스를 지원하는 사용자 정의 데이터베이스 매개변수 클래스입니다.
/// </summary>
public class DataParameter : DbParameter
{
    /// <summary>
    /// 매개변수의 크기
    /// </summary>
    public override int Size { get; set; }

    /// <summary>
    /// 매개변수의 데이터베이스 타입
    /// </summary>
    public override DbType DbType { get; set; }

    /// <summary>
    /// 매개변수의 방향 (입력/출력)
    /// </summary>
    public override ParameterDirection Direction { get; set; }

    /// <summary>
    /// NULL 허용 여부
    /// </summary>
    public override bool IsNullable { get; set; } = true;

    /// <summary>
    /// 매개변수 이름
    /// </summary>
    public override string ParameterName { get; set; } = string.Empty;

    /// <summary>
    /// 원본 컬럼 이름
    /// </summary>
    public override string SourceColumn { get; set; } = string.Empty;

    /// <summary>
    /// 매개변수 값
    /// </summary>
    public override object? Value { get; set; }

    /// <summary>
    /// 원본 컬럼의 NULL 매핑 여부
    /// </summary>
    public override bool SourceColumnNullMapping { get; set; }

    /// <summary>
    /// 특정 데이터베이스 프로바이더의 현재 데이터 타입
    /// </summary>
    public string CurrentDataType { get; set; } = string.Empty;

    /// <summary>
    /// 특정 데이터베이스 프로바이더의 사용자 정의 타입 이름
    /// </summary>
    public string UdtTypeName { get; set; } = string.Empty;

    /// <summary>
    /// DataParameter의 새 인스턴스를 초기화합니다.
    /// </summary>
    public DataParameter() { }

    /// <summary>
    /// 매개변수 이름과 값을 지정하여 DataParameter의 새 인스턴스를 초기화합니다.
    /// </summary>
    /// <param name="parameterName">매개변수 이름</param>
    /// <param name="value">매개변수 값</param>
    public DataParameter(string parameterName, object? value)
    {
        ParameterName = parameterName;
        Value = value;
        Direction = ParameterDirection.Input;
    }

    /// <summary>
    /// 매개변수 이름, 값, 크기를 지정하여 DataParameter의 새 인스턴스를 초기화합니다.
    /// </summary>
    /// <param name="parameterName">매개변수 이름</param>
    /// <param name="value">매개변수 값</param>
    /// <param name="size">매개변수 크기</param>
    public DataParameter(string parameterName, object? value, int size)
    {
        ParameterName = parameterName;
        Value = value;
        Size = size;
        Direction = ParameterDirection.Input;
    }

    /// <summary>
    /// 데이터 타입, 매개변수 이름, 값을 지정하여 DataParameter의 새 인스턴스를 초기화합니다.
    /// </summary>
    /// <param name="currentDataType">현재 데이터 타입</param>
    /// <param name="parameterName">매개변수 이름</param>
    /// <param name="value">매개변수 값</param>
    public DataParameter(string currentDataType, string parameterName, object? value)
    {
        CurrentDataType = currentDataType;
        ParameterName = parameterName;
        Value = value;
        Direction = ParameterDirection.Input;
    }

    /// <summary>
    /// DbType, 매개변수 이름, 값을 지정하여 DataParameter의 새 인스턴스를 초기화합니다.
    /// </summary>
    /// <param name="dbType">데이터베이스 타입</param>
    /// <param name="parameterName">매개변수 이름</param>
    /// <param name="value">매개변수 값</param>
    public DataParameter(DbType dbType, string parameterName, object? value)
    {
        DbType = dbType;
        ParameterName = parameterName;
        Value = value;
        Direction = ParameterDirection.Input;
    }

    /// <summary>
    /// 방향, 매개변수 이름, 값을 지정하여 DataParameter의 새 인스턴스를 초기화합니다.
    /// </summary>
    /// <param name="direction">매개변수 방향</param>
    /// <param name="parameterName">매개변수 이름</param>
    /// <param name="value">매개변수 값</param>
    public DataParameter(ParameterDirection direction, string parameterName, object? value)
    {
        Direction = direction;
        ParameterName = parameterName;
        Value = value;

        if (value is DateTime)
            DbType = DbType.DateTime;
    }

    /// <summary>
    /// 방향, 매개변수 이름, 값, 크기를 지정하여 DataParameter의 새 인스턴스를 초기화합니다.
    /// </summary>
    /// <param name="direction">매개변수 방향</param>
    /// <param name="parameterName">매개변수 이름</param>
    /// <param name="value">매개변수 값</param>
    /// <param name="size">매개변수 크기</param>
    public DataParameter(ParameterDirection direction, string parameterName, object? value, int size)
    {
        Direction = direction;
        ParameterName = parameterName;
        Value = value;
        Size = size;

        if (value is DateTime)
            DbType = DbType.DateTime;
    }

    /// <summary>
    /// 방향, DbType, 매개변수 이름, 값을 지정하여 DataParameter의 새 인스턴스를 초기화합니다.
    /// </summary>
    /// <param name="direction">매개변수 방향</param>
    /// <param name="dbType">데이터베이스 타입</param>
    /// <param name="parameterName">매개변수 이름</param>
    /// <param name="value">매개변수 값</param>
    public DataParameter(ParameterDirection direction, DbType dbType, string parameterName, object? value)
    {
        Direction = direction;
        DbType = dbType;
        ParameterName = parameterName;
        Value = value;
    }

    /// <summary>
    /// 데이터베이스 타입을 기본값(String)으로 재설정합니다.
    /// </summary>
    public override void ResetDbType()
    {
        DbType = DbType.String;
    }

    /// <summary>
    /// 매개변수의 문자열 표현을 반환합니다.
    /// </summary>
    /// <returns>매개변수 이름과 값의 문자열 표현</returns>
    public override string ToString()
    {
        return $"{ParameterName}: {Value}";
    }
}