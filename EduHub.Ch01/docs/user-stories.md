# User Stories - EduHub (Chuong 1)

He thong quan ly Cau lac bo & Su kien sinh vien Truong Dai hoc Dong Thap.

> Format: **As a [role], I want [feature], so that [value].**
> Moi story co toi thieu 2 tieu chi nghiem thu (acceptance criteria).

---

## Actor: Sinh vien

### US-01 - Dang ky tham gia cau lac bo
**As a** sinh vien,
**I want** dang ky tham gia mot cau lac bo,
**so that** toi co the nhan thong tin hoat dong va tham gia sinh hoat cua CLB do.

**Acceptance criteria:**
- [ ] Given toi la sinh vien da dang nhap, When toi nhan "Dang ky" tren trang chi tiet CLB, Then toi duoc them vao danh sach thanh vien cua CLB va thay thong bao "Dang ky thanh cong".
- [ ] Given toi da la thanh vien cua CLB, When toi nhan "Dang ky" lan nua, Then he thong hien thi loi "Ban da la thanh vien cua CLB nay" va khong tao ban ghi trung lap.

### US-02 - Dang ky tham du su kien
**As a** sinh vien (thanh vien CLB),
**I want** dang ky tham du mot su kien cua CLB,
**so that** ban to chuc biet toi se tham gia va toi nhan duoc xac nhan tham du.

**Acceptance criteria:**
- [ ] Given su kien con cho trong, When toi dang ky tham du, Then phieu dang ky duoc tao voi trang thai `Pending` va so cho trong cua su kien giam di 1.
- [ ] Given su kien da du so luong (dat Capacity), When toi dang ky, Then he thong tu choi voi thong bao "Su kien da du so luong dang ky".

### US-03 - Check-in khi tham du su kien
**As a** sinh vien,
**I want** check-in khi den du su kien,
**so that** ban to chuc ghi nhan toi da tham du va toi duoc tinh diem ren luyen.

**Acceptance criteria:**
- [ ] Given phieu dang ky cua toi da o trang thai `Confirmed`, When toi check-in (quet ma hoac diem danh), Then he thong tao mot ban ghi CheckIn voi thoi gian hien tai.
- [ ] Given toi check-in 2 lan cho cung mot su kien, When toi thuc hien check-in lan thu hai, Then he thong canh bao "Da check-in truoc do" va khong tao ban ghi moi.

---

## Actor: Ban CLB (nguoi quan ly CLB)

### US-04 - Tao su kien moi cho CLB
**As a** ban CLB,
**I want** tao su kien moi (ten, ngay to chuc, so luong toi da, hinh thuc online/offline),
**so that** thanh vien co the xem va dang ky tham du.

**Acceptance criteria:**
- [ ] Given toi nhap day du thong tin hop le (ten khong trong, ngay to chuc trong tuong lai, Capacity > 0), When toi nhan "Tao su kien", Then su kien duoc luu va hien thi trong danh sach su kien cua CLB.
- [ ] Given toi nhap Capacity <= 0 hoac ngay to chuc trong qua khu, When toi nhan "Tao su kien", Then he thong bao loi cu the va khong tao su kien.

### US-05 - Xem danh sach thanh vien CLB
**As a** ban CLB,
**I want** xem danh sach thanh vien cua CLB minh quan ly,
**so that** toi biet ai dang sinh hoat va lien he khi can.

**Acceptance criteria:**
- [ ] Given toi la ban CLB, When toi mo trang "Thanh vien", Then toi thay day du ho ten, email, ma sinh vien va ngay tham gia cua tung thanh vien.
- [ ] Given CLB co 0 thanh vien, When toi mo trang "Thanh vien", Then toi thay thong bao "Chua co thanh vien nao" thay vi bang trong.

---

## Actor: Admin

### US-06 - Xuat bao cao tham du su kien
**As a** admin,
**I want** xuat bao cao tham du cua mot su kien (so luot dang ky, so luot check-in, danh sach ten),
**so that** toi danh gia hieu qua to chuc va luu ho so hoat dong.

**Acceptance criteria:**
- [ ] Given su kien da dien ra, When toi nhan "Xuat bao cao", Then bao cao hien thi dung so luot dang ky, so luot check-in va danh sach ho ten nguoi tham du.
- [ ] Given su kien chua co ai dang ky, When toi nhan "Xuat bao cao", Then bao cao hien thi so 0 va thong bao "Chua co du lieu".

---

## Ghi chu traceability

| Story | Class lien quan |
|-------|-----------------|
| US-01 | `Club.AddMember()`, `Member` |
| US-02 | `Event.HasCapacity()`, `Registration` |
| US-03 | `CheckIn`, `Registration.Status` |
| US-04 | `Event.Create()`, `Club` |
| US-05 | `Club.Members`, `Club.MemberCount()` |
| US-06 | `Registration`, `CheckIn`, `CheckIn.Summary()` |
