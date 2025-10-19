-- =========================================
-- Insert Doctors
-- =========================================
INSERT INTO Doctors (Id, Name, Department, Phone, Fee)
VALUES 
    ('11111111-1111-1111-1111-111111111111', 'Dr. Ahmed Khan', 'Cardiology', '01710000101', 800),
    ('22222222-2222-2222-2222-222222222222', 'Dr. Sara Rahman', 'Neurology', '01710000102', 900),
    ('33333333-3333-3333-3333-333333333333', 'Dr. Tanvir Islam', 'Orthopedics', '01710000103', 750),
    ('44444444-4444-4444-4444-444444444444', 'Dr. Farhana Akter', 'Pediatrics', '01710000104', 700),
    ('55555555-5555-5555-5555-555555555555', 'Dr. Rafiq Uddin', 'Dermatology', '01710000105', 650);

-- =========================================
-- Insert Patients
-- =========================================
INSERT INTO Patients (Id, Name, Phone, Age, Gender, Address, CreatedDate)
VALUES
    ('aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa', 'Rashed Alam', '01810001001', 25, 'Male', 'Dhaka', GETDATE()),
    ('bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb', 'Mousumi Begum', '01810001002', 30, 'Female', 'Chittagong', GETDATE()),
    ('cccccccc-cccc-cccc-cccc-cccccccccccc', 'Jahidul Islam', '01810001003', 28, 'Male', 'Khulna', GETDATE()),
    ('dddddddd-dddd-dddd-dddd-dddddddddddd', 'Tasnia Akter', '01810001004', 22, 'Female', 'Rajshahi', GETDATE()),
    ('eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee', 'Sabbir Hossain', '01810001005', 35, 'Male', 'Barishal', GETDATE()),
    ('ffffffff-ffff-ffff-ffff-ffffffffffff', 'Nusrat Jahan', '01810001006', 27, 'Female', 'Sylhet', GETDATE()),
    ('11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa', 'Rafi Ahmed', '01810001007', 40, 'Male', 'Comilla', GETDATE()),
    ('22222222-bbbb-bbbb-bbbb-bbbbbbbbbbbb', 'Purnima Roy', '01810001008', 29, 'Female', 'Mymensingh', GETDATE()),
    ('33333333-cccc-cccc-cccc-cccccccccccc', 'Fahim Chowdhury', '01810001009', 32, 'Male', 'Tangail', GETDATE()),
    ('44444444-dddd-dddd-dddd-dddddddddddd', 'Taslima Khatun', '01810001010', 26, 'Female', 'Gazipur', GETDATE()),
    ('55555555-eeee-eeee-eeee-eeeeeeeeeeee', 'Shuvo Karim', '01810001011', 31, 'Male', 'Narayanganj', GETDATE()),
    ('66666666-ffff-ffff-ffff-ffffffffffff', 'Laboni Sultana', '02410001012', 24, 'Female', 'CoxsBazar', GETDATE()),
    ('77777777-1111-1111-1111-111111111111', 'Imran Hossain', '01810001013', 33, 'Male', 'Bogra', GETDATE()),
    ('88888888-2222-2222-2222-222222222222', 'Sumaiya Begum', '01810001014', 28, 'Female', 'Jessore', GETDATE()),
    ('99999999-3333-3333-3333-333333333333', 'Rakibul Hasan', '01810001015', 36, 'Male', 'Dinajpur', GETDATE());

-- =========================================
-- Insert Appointments
-- 6 per doctor, pairs share same date & TenantId
-- =========================================

-- Doctor 1
INSERT INTO Appointments (Id, PatientId, DoctorId, AppointmentDate, Status, TokenNumber, TenantId)
VALUES
    (NEWID(), 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa', '11111111-1111-1111-1111-111111111111', GETDATE(), 'Pending', 1, 'Tenant1'),
    (NEWID(), 'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb', '11111111-1111-1111-1111-111111111111', GETDATE(), 'Pending', 2, 'Tenant1'),
    (NEWID(), 'cccccccc-cccc-cccc-cccc-cccccccccccc', '11111111-1111-1111-1111-111111111111', DATEADD(DAY,1,GETDATE()), 'Pending', 3, 'Tenant2'),
    (NEWID(), 'dddddddd-dddd-dddd-dddd-dddddddddddd', '11111111-1111-1111-1111-111111111111', DATEADD(DAY,1,GETDATE()), 'Pending', 4, 'Tenant2'),
    (NEWID(), 'eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee', '11111111-1111-1111-1111-111111111111', DATEADD(DAY,2,GETDATE()), 'Pending', 5, 'Tenant3'),
    (NEWID(), 'ffffffff-ffff-ffff-ffff-ffffffffffff', '11111111-1111-1111-1111-111111111111', DATEADD(DAY,2,GETDATE()), 'Pending', 6, 'Tenant3');

-- Doctor 2
INSERT INTO Appointments (Id, PatientId, DoctorId, AppointmentDate, Status, TokenNumber, TenantId)
VALUES
    (NEWID(), '11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa', '22222222-2222-2222-2222-222222222222', GETDATE(), 'Pending', 1, 'Tenant1'),
    (NEWID(), '22222222-bbbb-bbbb-bbbb-bbbbbbbbbbbb', '22222222-2222-2222-2222-222222222222', GETDATE(), 'Pending', 2, 'Tenant1'),
    (NEWID(), '33333333-cccc-cccc-cccc-cccccccccccc', '22222222-2222-2222-2222-222222222222', DATEADD(DAY,1,GETDATE()), 'Pending', 3, 'Tenant2'),
    (NEWID(), '44444444-dddd-dddd-dddd-dddddddddddd', '22222222-2222-2222-2222-222222222222', DATEADD(DAY,1,GETDATE()), 'Pending', 4, 'Tenant2'),
    (NEWID(), '55555555-eeee-eeee-eeee-eeeeeeeeeeee', '22222222-2222-2222-2222-222222222222', DATEADD(DAY,2,GETDATE()), 'Pending', 5, 'Tenant3'),
    (NEWID(), '66666666-ffff-ffff-ffff-ffffffffffff', '22222222-2222-2222-2222-222222222222', DATEADD(DAY,2,GETDATE()), 'Pending', 6, 'Tenant3');

-- Doctor 3
INSERT INTO Appointments (Id, PatientId, DoctorId, AppointmentDate, Status, TokenNumber, TenantId)
VALUES
    (NEWID(), '77777777-1111-1111-1111-111111111111', '33333333-3333-3333-3333-333333333333', GETDATE(), 'Pending', 1, 'Tenant1'),
    (NEWID(), '88888888-2222-2222-2222-222222222222', '33333333-3333-3333-3333-333333333333', GETDATE(), 'Pending', 2, 'Tenant1'),
    (NEWID(), '99999999-3333-3333-3333-333333333333', '33333333-3333-3333-3333-333333333333', DATEADD(DAY,1,GETDATE()), 'Pending', 3, 'Tenant2'),
    (NEWID(), 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa', '33333333-3333-3333-3333-333333333333', DATEADD(DAY,1,GETDATE()), 'Pending', 4, 'Tenant2'),
    (NEWID(), 'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb', '33333333-3333-3333-3333-333333333333', DATEADD(DAY,2,GETDATE()), 'Pending', 5, 'Tenant3'),
    (NEWID(), 'cccccccc-cccc-cccc-cccc-cccccccccccc', '33333333-3333-3333-3333-333333333333', DATEADD(DAY,2,GETDATE()), 'Pending', 6, 'Tenant3');

-- Doctor 4
INSERT INTO Appointments (Id, PatientId, DoctorId, AppointmentDate, Status, TokenNumber, TenantId)
VALUES
    (NEWID(), 'dddddddd-dddd-dddd-dddd-dddddddddddd', '44444444-4444-4444-4444-444444444444', GETDATE(), 'Pending', 1, 'Tenant1'),
    (NEWID(), 'eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee', '44444444-4444-4444-4444-444444444444', GETDATE(), 'Pending', 2, 'Tenant1'),
    (NEWID(), 'ffffffff-ffff-ffff-ffff-ffffffffffff', '44444444-4444-4444-4444-444444444444', DATEADD(DAY,1,GETDATE()), 'Pending', 3, 'Tenant2'),
    (NEWID(), '11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa', '44444444-4444-4444-4444-444444444444', DATEADD(DAY,1,GETDATE()), 'Pending', 4, 'Tenant2'),
    (NEWID(), '22222222-bbbb-bbbb-bbbb-bbbbbbbbbbbb', '44444444-4444-4444-4444-444444444444', DATEADD(DAY,2,GETDATE()), 'Pending', 5, 'Tenant3'),
    (NEWID(), '33333333-cccc-cccc-cccc-cccccccccccc', '44444444-4444-4444-4444-444444444444', DATEADD(DAY,2,GETDATE()), 'Pending', 6, 'Tenant3');

-- Doctor 5
INSERT INTO Appointments (Id, PatientId, DoctorId, AppointmentDate, Status, TokenNumber, TenantId)
VALUES
    (NEWID(), '44444444-dddd-dddd-dddd-dddddddddddd', '55555555-5555-5555-5555-555555555555', GETDATE(), 'Pending', 1, 'Tenant1'),
    (NEWID(), '55555555-eeee-eeee-eeee-eeeeeeeeeeee', '55555555-5555-5555-5555-555555555555', GETDATE(), 'Pending', 2, 'Tenant1'),
    (NEWID(), '66666666-ffff-ffff-ffff-ffffffffffff', '55555555-5555-5555-5555-555555555555', DATEADD(DAY,1,GETDATE()), 'Pending', 3, 'Tenant2'),
    (NEWID(), '77777777-1111-1111-1111-111111111111', '55555555-5555-5555-5555-555555555555', DATEADD(DAY,1,GETDATE()), 'Pending', 4, 'Tenant2'),
    (NEWID(), '88888888-2222-2222-2222-222222222222', '55555555-5555-5555-5555-555555555555', DATEADD(DAY,2,GETDATE()), 'Pending', 5, 'Tenant3'),
    (NEWID(), '99999999-3333-3333-3333-333333333333', '55555555-5555-5555-5555-555555555555', DATEADD(DAY,2,GETDATE()), 'Pending', 6, 'Tenant3');
