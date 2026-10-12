namespace EduHub.Ch01.Domain;

/// <summary>
/// Hinh thuc ghi nhan diem danh tai su kien.
/// </summary>
public enum CheckInMethod
{
    /// <summary>Diem danh thu cong (ban to chuc tick ten).</summary>
    Manual,

    /// <summary>Quet ma QR tai cong su kien.</summary>
    QrCode
}
