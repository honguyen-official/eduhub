namespace EduHub.Ch01.Domain;

/// <summary>
/// Dai dien cho mot su kien do cau lac bo to chuc.
/// </summary>
public sealed class Event
{
    /// <summary>Khoa chinh cua su kien.</summary>
    public Guid Id { get; private set; }

    /// <summary>Khoa ngoai tro toi CLB to chuc su kien.</summary>
    public Guid ClubId { get; private set; }

    /// <summary>Tieu de su kien.</summary>
    public string Title { get; private set; }

    /// <summary>Ngay dien ra su kien.</summary>
    public DateTime Date { get; private set; }

    /// <summary>So luong nguoi tham du toi da.</summary>
    public int Capacity { get; private set; }

    /// <summary><c>true</c> neu su kien to chuc truc tuyen; <c>false</c> neu truc tiep.</summary>
    public bool IsOnline { get; private set; }

    private Event(Guid id, Guid clubId, string title, DateTime date, int capacity, bool isOnline)
    {
        Id = id;
        ClubId = clubId;
        Title = title;
        Date = date;
        Capacity = capacity;
        IsOnline = isOnline;
    }

    /// <summary>
    /// Tao mot su kien moi, dong thoi kiem tra tinh hop le cua du lieu dau vao.
    /// </summary>
    /// <param name="clubId">Id cua CLB to chuc (khong duoc la Guid rong).</param>
    /// <param name="title">Tieu de su kien (khong duoc trong).</param>
    /// <param name="date">Ngay dien ra (phai trong tuong lai).</param>
    /// <param name="capacity">So luong toi da (phai lon hon 0).</param>
    /// <param name="isOnline"><c>true</c> neu to chuc truc tuyen.</param>
    /// <returns>Doi tuong <see cref="Event"/> hop le.</returns>
    /// <exception cref="ArgumentException">Khi du lieu dau vao khong hop le.</exception>
    public static Event Create(Guid clubId, string title, DateTime date, int capacity, bool isOnline)
    {
        if (clubId == Guid.Empty)
        {
            throw new ArgumentException("Su kien phai thuoc ve mot CLB hop le.", nameof(clubId));
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Tieu de su kien khong duoc de trong.", nameof(title));
        }

        if (date <= DateTime.UtcNow)
        {
            throw new ArgumentException("Ngay dien ra su kien phai trong tuong lai.", nameof(date));
        }

        if (capacity <= 0)
        {
            throw new ArgumentException("Suc chua su kien phai lon hon 0.", nameof(capacity));
        }

        return new Event(
            Guid.NewGuid(),
            clubId,
            title.Trim(),
            date,
            capacity,
            isOnline);
    }

    /// <summary>
    /// Kiem tra su kien con cho trong de tiep nhan them dang ky hay khong.
    /// </summary>
    /// <param name="registeredCount">So luot dang ky hien tai.</param>
    /// <returns><c>true</c> neu con cho trong; nguoc lai <c>false</c>.</returns>
    public bool HasCapacity(int registeredCount)
    {
        return registeredCount < Capacity;
    }
}
