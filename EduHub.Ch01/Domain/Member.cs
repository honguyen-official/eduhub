namespace EduHub.Ch01.Domain;

/// <summary>
/// Dai dien cho mot sinh vien la thanh vien cua cau lac bo.
/// </summary>
public sealed class Member
{
    /// <summary>Khoa chinh cua thanh vien.</summary>
    public Guid Id { get; private set; }

    /// <summary>Ho va ten cua thanh vien.</summary>
    public string Name { get; private set; }

    /// <summary>Dia chi email cua thanh vien.</summary>
    public string Email { get; private set; }

    /// <summary>Ma sinh vien (duy nhat trong pham vi truong).</summary>
    public string StudentCode { get; private set; }

    /// <summary>Thoi diem thanh vien gia nhap CLB.</summary>
    public DateTime JoinedAt { get; private set; }

    private Member(Guid id, string name, string email, string studentCode, DateTime joinedAt)
    {
        Id = id;
        Name = name;
        Email = email;
        StudentCode = studentCode;
        JoinedAt = joinedAt;
    }

    /// <summary>
    /// Tao mot thanh vien moi, dong thoi kiem tra tinh hop le cua du lieu dau vao.
    /// </summary>
    /// <param name="name">Ho va ten (khong duoc trong).</param>
    /// <param name="email">Email (phai chua ky tu @).</param>
    /// <param name="studentCode">Ma sinh vien (khong duoc trong).</param>
    /// <returns>Doi tuong <see cref="Member"/> hop le.</returns>
    /// <exception cref="ArgumentException">Khi du lieu dau vao khong hop le.</exception>
    public static Member Create(string name, string email, string studentCode)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Ten thanh vien khong duoc de trong.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
        {
            throw new ArgumentException("Email khong hop le (phai chua ky tu @).", nameof(email));
        }

        if (string.IsNullOrWhiteSpace(studentCode))
        {
            throw new ArgumentException("Ma sinh vien khong duoc de trong.", nameof(studentCode));
        }

        return new Member(
            Guid.NewGuid(),
            name.Trim(),
            email.Trim(),
            studentCode.Trim(),
            DateTime.UtcNow);
    }

    /// <summary>
    /// Kiem tra thanh vien con dang sinh hoat hay khong.
    /// Mot thanh vien duoc coi la dang hoat dong khi thoi diem gia nhap da dien ra
    /// (khong phai ban ghi tao truoc voi ngay tham gia trong tuong lai).
    /// </summary>
    /// <returns><c>true</c> neu thanh vien dang hoat dong; nguoc lai <c>false</c>.</returns>
    public bool IsActive()
    {
        return JoinedAt <= DateTime.UtcNow;
    }
}
