# So do lop so bo - EduHub (Chuong 1)

```mermaid
classDiagram
    class Member {
        +Guid Id
        +string Name
        +string Email
        +string StudentCode
        +DateTime JoinedAt
        +static Create(name, email, studentCode) Member
        +IsActive() bool
    }

    class Club {
        +Guid Id
        +string Name
        +string Description
        +DateTime CreatedAt
        +IReadOnlyList~Member~ Members
        +static Create(name, description) Club
        +AddMember(member) void
        +MemberCount() int
    }

    class Event {
        +Guid Id
        +Guid ClubId
        +string Title
        +DateTime Date
        +int Capacity
        +bool IsOnline
        +static Create(clubId, title, date, capacity, isOnline) Event
        +HasCapacity(registeredCount) bool
    }

    class Registration {
        +Guid Id
        +Guid MemberId
        +Guid EventId
        +DateTime RegisteredAt
        +RegistrationStatus Status
        +static Create(memberId, eventId) Registration
        +Confirm() void
        +Cancel() void
    }

    class CheckIn {
        +Guid Id
        +Guid RegistrationId
        +DateTime CheckedInAt
        +CheckInMethod Method
        +static Create(registrationId, method) CheckIn
        +Summary() string
    }

    class RegistrationStatus {
        <<enumeration>>
        Pending
        Confirmed
        Cancelled
    }

    class CheckInMethod {
        <<enumeration>>
        Manual
        QrCode
    }

    %% --- Quan he ---
    Club "1" --> "*" Member : co (owns)
    Club "1" --> "*" Event : to chuc
    Event "1" --> "*" Registration : nhan
    Member "1" --> "*" Registration : dang ky
    Registration "1" --> "0..1" CheckIn : ghi nhan diem danh
    Registration ..> RegistrationStatus : trang thai
    CheckIn ..> CheckInMethod : hinh thuc

    %% Ghi chu boi so
    note for Registration "Bang noi nhieu-nhieu\nMember - Event"
```

## Giai thich thiet ke

| Quan he | Boi so | Y nghia |
|---------|--------|---------|
| Club - Member | 1 - * | Mot CLB co nhieu thanh vien; trong pham vi Ch01, moi ban ghi `Member` gan voi 1 CLB quan ly (du cho luong demo) |
| Club - Event | 1 - * | Mot CLB to chuc nhieu su kien; moi su kien thuoc dung 1 CLB (`ClubId` - khoa ngoai) |
| Event - Registration | 1 - * | Mot su kien nhan nhieu phieu dang ky; moi phieu thuoc dung 1 su kien (`EventId`) |
| Member - Registration | 1 - * | Mot thanh vien dang ky nhieu su kien; moi phieu thuoc dung 1 thanh vien (`MemberId`) |
| Registration - CheckIn | 1 - 0..1 | Moi phieu dang ky duoc check-in toi da 1 lan (`RegistrationId`) |

- `Registration` la **bang noi** (association class) cho quan he nhieu-nhieu giua `Member` va `Event`, dong thoi mang trang thai nghiep vu (`Pending` / `Confirmed` / `Cancelled`).
- `RegistrationStatus` va `CheckInMethod` la **enum** - khong dung chuoi rai rac (tuan thu yeu cau "khong magic string").
- Tat ca class dung **factory method** `Create(...)` + constructor private; property `private set`/`init`; validation tap trung trong `Create(...)`.
