﻿if not exists (select 1 from dbo.[User])
begin
		insert into dbo.[User] (FirstName,LastName)
		values ('Tim', 'Corey'),
		('Sue', 'Storm'),
		('Jessica', 'Branco')

end