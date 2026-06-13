--Câu 3.1
SELECT A.TenNV, B.TenDuAn, C.TenDiaDiem, D.NgayThamGia 
FROM NhanVien A, DuAn B, DiaDiem C, NhanVien_Duan D
WHERE A.Id = D.MaNhanVien AND B.Id = D.MaDuAn AND B.MaDiaDiem = C.Id

--Câu 3.2
SELECT A.TenNV, B.TenPB, C.TenDuAn, D.NgayThamGia
FROM NhanVien A, PhongBan B, DuAn C, NhanVien_Duan D
WHERE A.Id = D.MaNhanVien AND C.Id = D.MaDuAn AND B.Id = A.MaPB

--Câu 3.3
SELECT A.TenNV, B.TenPB, C.TenDuAn, D.TenDiaDiem
FROM NhanVien A, PhongBan B, DuAn C, DiaDiem D, NhanVien_Duan E
WHERE A.MaPB = B.Id
AND A.Id = E.MaNhanVien AND C.Id = E.MaDuAn
AND C.MaDiaDiem = D.Id