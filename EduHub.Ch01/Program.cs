using EduHub.Ch01.Domain;

const string Separator = "========================================";
const string ClubName = "Tech Club";
const string ClubDescription = "Cau lac bo Cong nghe - noi sinh vien DH Dong Thap " +
                              "kham pha lap trinh, AI va phat trien phan mem.";
const string EventTitle = "Workshop AI: Nhap mon Machine Learning";
const int EventCapacity = 50;
const bool EventIsOnline = false;
const int ExpectedRegistrationCount = 2;
const int ExpectedCheckInCount = 1;

void PrintStep(string title)
{
    Console.WriteLine();
    Console.WriteLine(Separator);
    Console.WriteLine(title);
    Console.WriteLine(Separator);
}

// ---- Buoc 1: Tao CLB "Tech Club" ----
PrintStep("BUOC 1 - Tao cau lac bo");
var club = Club.Create(ClubName, ClubDescription);
Console.WriteLine($"Da tao CLB '{club.Name}' (Id: {club.Id}).");
Console.WriteLine($"Mo ta: {club.Description}");

// ---- Buoc 2: Them 3 thanh vien ----
PrintStep("BUOC 2 - Them 3 thanh vien vao CLB");
var members = new[]
{
    Member.Create("Tran Anh Tuan", "anhtuan@student.dthu.edu.vn", "DHDT24001"),
    Member.Create("Le Bao Ngoc", "baongoc@student.dthu.edu.vn", "DHDT24002"),
    Member.Create("Pham Minh Duc", "minhduc@student.dthu.edu.vn", "DHDT24003"),
};

foreach (var member in members)
{
    club.AddMember(member);
    Console.WriteLine($"- {member.Name} (MSSV: {member.StudentCode}, Email: {member.Email}) " +
                      $"-> Dang hoat dong: {member.IsActive()}");
}
Console.WriteLine($"Tong so thanh vien CLB: {club.MemberCount()}");

// ---- Buoc 3: Tao 1 su kien ----
PrintStep("BUOC 3 - Tao su kien cho CLB");
var workshopDate = new DateTime(2026, 11, 15, 8, 0, 0, DateTimeKind.Utc);
var aiWorkshop = Event.Create(club.Id, EventTitle, workshopDate, EventCapacity, EventIsOnline);
Console.WriteLine($"Su kien: '{aiWorkshop.Title}'");
Console.WriteLine($"Ngay to chuc: {aiWorkshop.Date:dd/MM/yyyy HH:mm} | " +
                  $"Suc chua: {aiWorkshop.Capacity} | " +
                  $"Hinh thuc: {(aiWorkshop.IsOnline ? "Truc tuyen" : "Truc tiep")}");

// ---- Buoc 4: 2 thanh vien dang ky -> Pending -> Confirm ----
PrintStep("BUOC 4 - 2 thanh vien dang ky tham du su kien");
var registrations = new List<Registration>();

foreach (var member in members.Take(ExpectedRegistrationCount))
{
    if (!aiWorkshop.HasCapacity(registrations.Count))
    {
        Console.WriteLine($"Su kien da du so luong, khong the nhan them dang ky cua {member.Name}.");
        continue;
    }

    var registration = Registration.Create(member.Id, aiWorkshop.Id);
    Console.WriteLine($"- {member.Name} dang ky -> Trang thai: {registration.Status}");

    registration.Confirm();
    registrations.Add(registration);
    Console.WriteLine($"  -> Da xac nhan (Confirm) -> Trang thai: {registration.Status}");
}

// ---- Buoc 5: 1 thanh vien check-in ----
PrintStep("BUOC 5 - Thanh vien check-in tai su kien");
var checkIns = new List<CheckIn>();

foreach (var registration in registrations.Take(ExpectedCheckInCount))
{
    if (registration.Status != RegistrationStatus.Confirmed)
    {
        Console.WriteLine("Phieu chua xac nhan nen khong the check-in.");
        continue;
    }

    var checkIn = CheckIn.Create(registration.Id, CheckInMethod.QrCode);
    checkIns.Add(checkIn);
    Console.WriteLine(checkIn.Summary());
}

// ---- Buoc 6: Bao cao tham du ----
PrintStep("BUOC 6 - Bao cao tham du su kien");
Console.WriteLine($"Su kien: {aiWorkshop.Title}");
Console.WriteLine($"So luot dang ky: {registrations.Count}");
Console.WriteLine($"So luot check-in: {checkIns.Count}");
Console.WriteLine("Danh sach thanh vien da dang ky:");

foreach (var registration in registrations)
{
    var member = members.Single(m => m.Id == registration.MemberId);
    var checkInStatus = checkIns.Any(c => c.RegistrationId == registration.Id)
        ? "Da check-in"
        : "Chua check-in";
    Console.WriteLine($"- {member.Name} ({member.StudentCode}) - " +
                      $"Trang thai: {registration.Status} - {checkInStatus}");
}

// ---- Buoc 7 (bo sung): Thu thao tac vi pham luat nghiep vu ----
PrintStep("BUOC 7 - Thu nghiem luat nghiep vu (Confirm phieu da huy)");
var doomedRegistration = Registration.Create(members[2].Id, aiWorkshop.Id);
doomedRegistration.Cancel();
Console.WriteLine($"Da tao va huy phieu cua {members[2].Name} -> Trang thai: {doomedRegistration.Status}");

try
{
    doomedRegistration.Confirm();
    Console.WriteLine("LOI: He thong da cho phep xac nhan phieu da huy!");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Da chan dung luat nghiep vu: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine(Separator);
Console.WriteLine("Demo hoan tat - moi luong nghiep vu chay dung nhu ky vong.");
Console.WriteLine(Separator);
