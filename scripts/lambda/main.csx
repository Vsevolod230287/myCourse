
Func<DateTime, bool> canDrive = dob => dob.AddYears(18) <= DateTime.Today;

DateTime dob = new DateTime(2000, 12, 25);

bool result = canDrive(dob);
Console.WriteLine(result);


Action<DateTime> s = date => Console.WriteLine(date);
DateTime a = DateTime.Today;

s(a);

