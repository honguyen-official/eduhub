namespace EduHub.Ch01.Domain;

/// <summary>
/// Ban ghi diem danh: xac nhan mot phieu dang ky da thuc su tham du su kien.
/// Moi phieu dang ky co toi da mot ban ghi check-in (quan he 1 - 0..1).
/// </summary>
public sealed class CheckIn
{
    /// <summary>Khoa chinh cua ban ghi check-in.</summary>
    public Guid Id { get; private set; }

    /// <summary>Khoa ngoai tro toi phieu dang ky duoc diem danh.</summary>
    public Guid RegistrationId { get; private set; }

    /// <summary>Thoi diem diem danh.</summary>
    public DateTime CheckedInAt { get; private set; }

    /// <summary>Hinh thuc diem danh (thu cong hoac quet QR).</summary>
    public CheckInMethod Method { get; private set; }

    private CheckIn(Guid id, Guid registrationId, DateTime checkedInAt, CheckInMethod method)
    {
        Id = id;
        RegistrationId = registrationId;
        CheckedInAt = checkedInAt;
        Method = method;
    }

    /// <summary>
    /// Tao mot ban ghi check-in cho phieu dang ky da xac nhan.
    /// </summary>
    /// <param name="registrationId">Id cua phieu dang ky (khong duoc la Guid rong).</param>
    /// <param name="method">Hinh thuc diem danh.</param>
    /// <returns>Doi tuong <see cref="CheckIn"/> hop le.</returns>
    /// <exception cref="ArgumentException">Khi Id phieu dang ky khong hop le.</exception>
    public static CheckIn Create(Guid registrationId, CheckInMethod method)
    {
        if (registrationId == Guid.Empty)
        {
            throw new ArgumentException("Check-in phai gan voi mot phieu dang ky hop le.", nameof(registrationId));
        }

        return new CheckIn(
            Guid.NewGuid(),
            registrationId,
            DateTime.UtcNow,
            method);
    }

    /// <summary>
    /// Tom tat thong tin diem danh duoi dang chuoi de doc.
    /// </summary>
    /// <returns>Chuoi mo ta ban ghi check-in.</returns>
    public string Summary()
    {
        return $"Check-in [{Id.ToString("N")[..8]}] - Phieu {RegistrationId.ToString("N")[..8]} " +
               $"luc {CheckedInAt:dd/MM/yyyy HH:mm:ss} (hinh thuc: {Method}).";
    }
}
