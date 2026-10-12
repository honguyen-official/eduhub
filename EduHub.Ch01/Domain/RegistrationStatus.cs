namespace EduHub.Ch01.Domain;

/// <summary>
/// Trang thai cua mot phieu dang ky tham du su kien.
/// </summary>
public enum RegistrationStatus
{
    /// <summary>Phieu vua duoc tao, dang cho xac nhan.</summary>
    Pending,

    /// <summary>Phieu da duoc xac nhan, thanh vien duoc phep check-in.</summary>
    Confirmed,

    /// <summary>Phieu da bi huy, khong con hieu luc.</summary>
    Cancelled
}
