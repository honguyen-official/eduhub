namespace EduHub.Ch01.Domain;

/// <summary>
/// Dai dien cho mot cau lac bo sinh vien - don vi to chuc su kien va quan ly thanh vien.
/// </summary>
public sealed class Club
{
    private readonly List<Member> _members = new();

    /// <summary>Khoa chinh cua CLB.</summary>
    public Guid Id { get; private set; }

    /// <summary>Ten cau lac bo.</summary>
    public string Name { get; private set; }

    /// <summary>Mo ta ngan ve CLB.</summary>
    public string Description { get; private set; }

    /// <summary>Thoi diem CLB duoc tao tren he thong.</summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>Danh sach thanh vien cua CLB (chi doc tu ben ngoai).</summary>
    public IReadOnlyList<Member> Members => _members.AsReadOnly();

    private Club(Guid id, string name, string description, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Description = description;
        CreatedAt = createdAt;
    }

    /// <summary>
    /// Tao mot cau lac bo moi, dong thoi kiem tra tinh hop le cua du lieu dau vao.
    /// </summary>
    /// <param name="name">Ten CLB (khong duoc trong).</param>
    /// <param name="description">Mo ta CLB (khong duoc trong).</param>
    /// <returns>Doi tuong <see cref="Club"/> hop le.</returns>
    /// <exception cref="ArgumentException">Khi ten hoac mo ta trong.</exception>
    public static Club Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Ten CLB khong duoc de trong.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Mo ta CLB khong duoc de trong.", nameof(description));
        }

        return new Club(
            Guid.NewGuid(),
            name.Trim(),
            description.Trim(),
            DateTime.UtcNow);
    }

    /// <summary>
    /// Them mot thanh vien vao CLB. Khong cho phep them trung (theo <see cref="Member.Id"/>).
    /// </summary>
    /// <param name="member">Thanh vien can them.</param>
    /// <exception cref="ArgumentNullException">Khi <paramref name="member"/> la null.</exception>
    /// <exception cref="InvalidOperationException">Khi thanh vien da ton tai trong CLB.</exception>
    public void AddMember(Member member)
    {
        ArgumentNullException.ThrowIfNull(member);

        if (_members.Any(m => m.Id == member.Id))
        {
            throw new InvalidOperationException(
                $"Thanh vien '{member.Name}' da la thanh vien cua CLB '{Name}'.");
        }

        _members.Add(member);
    }

    /// <summary>
    /// Dem so thanh vien hien tai cua CLB.
    /// </summary>
    /// <returns>So luong thanh vien.</returns>
    public int MemberCount()
    {
        return _members.Count;
    }
}
