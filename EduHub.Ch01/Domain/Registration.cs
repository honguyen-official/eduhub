namespace EduHub.Ch01.Domain;

/// <summary>
/// Phieu dang ky cua mot thanh vien tham du mot su kien.
/// Dong vai tro bang noi cho quan he nhieu-nhieu giua <see cref="Member"/> va <see cref="Event"/>,
/// dong thoi mang trang thai nghiep vu cua luot dang ky.
/// </summary>
public sealed class Registration
{
    /// <summary>Khoa chinh cua phieu dang ky.</summary>
    public Guid Id { get; private set; }

    /// <summary>Khoa ngoai tro toi thanh vien dang ky.</summary>
    public Guid MemberId { get; private set; }

    /// <summary>Khoa ngoai tro toi su kien duoc dang ky.</summary>
    public Guid EventId { get; private set; }

    /// <summary>Thoi diem tao phieu dang ky.</summary>
    public DateTime RegisteredAt { get; private set; }

    /// <summary>Trang thai hien tai cua phieu dang ky.</summary>
    public RegistrationStatus Status { get; private set; }

    private Registration(Guid id, Guid memberId, Guid eventId, DateTime registeredAt, RegistrationStatus status)
    {
        Id = id;
        MemberId = memberId;
        EventId = eventId;
        RegisteredAt = registeredAt;
        Status = status;
    }

    /// <summary>
    /// Tao mot phieu dang ky moi o trang thai <see cref="RegistrationStatus.Pending"/>.
    /// </summary>
    /// <param name="memberId">Id cua thanh vien (khong duoc la Guid rong).</param>
    /// <param name="eventId">Id cua su kien (khong duoc la Guid rong).</param>
    /// <returns>Phieu dang ky o trang thai Pending.</returns>
    /// <exception cref="ArgumentException">Khi Id thanh vien hoac su kien khong hop le.</exception>
    public static Registration Create(Guid memberId, Guid eventId)
    {
        if (memberId == Guid.Empty)
        {
            throw new ArgumentException("Phieu dang ky phai gan voi mot thanh vien hop le.", nameof(memberId));
        }

        if (eventId == Guid.Empty)
        {
            throw new ArgumentException("Phieu dang ky phai gan voi mot su kien hop le.", nameof(eventId));
        }

        return new Registration(
            Guid.NewGuid(),
            memberId,
            eventId,
            DateTime.UtcNow,
            RegistrationStatus.Pending);
    }

    /// <summary>
    /// Xac nhan phieu dang ky (Pending -> Confirmed).
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Khi phieu khong o trang thai Pending (da xac nhan hoac da huy).
    /// </exception>
    public void Confirm()
    {
        if (Status != RegistrationStatus.Pending)
        {
            throw new InvalidOperationException(
                $"Khong the xac nhan phieu dang ky o trang thai '{Status}'. Chi phieu Pending moi duoc xac nhan.");
        }

        Status = RegistrationStatus.Confirmed;
    }

    /// <summary>
    /// Huy phieu dang ky (Pending/Confirmed -> Cancelled).
    /// </summary>
    /// <exception cref="InvalidOperationException">Khi phieu da o trang thai Cancelled.</exception>
    public void Cancel()
    {
        if (Status == RegistrationStatus.Cancelled)
        {
            throw new InvalidOperationException("Phieu dang ky da bi huy truoc do, khong the huy lai.");
        }

        Status = RegistrationStatus.Cancelled;
    }
}
