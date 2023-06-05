

if not exists (select * from sysobjects where name='#Statistics')
create table #Statistics (
_BMI INT,
_Descript NVARCHAR(60)
    )
go


INSERT INTO #Statistics (_BMI, _Descript) VALUES 
(ROUND((SELECT SUM (CASE WHEN BMI < 16 THEN 1.0 ELSE 0 END )FROM Patients)/* Количество человек из первой категории */
		/ 
		( SELECT COUNT(*) FROM Patients )/* Общее количество человек */
		*100 , 0 ),
		'Выраженный дефицит массы тела'),
		
(ROUND((SELECT SUM (CASE WHEN BMI BETWEEN 16.1 AND 18.5 THEN 1.0 ELSE 0 END)FROM Patients) / ( SELECT COUNT(*) FROM Patients)*100, 0 ),
		'Недостаточная (дефицит) масса тела'),

(ROUND((SELECT SUM (CASE WHEN BMI BETWEEN 18.6 AND 25 THEN 1.0 ELSE 0 END) FROM Patients) / ( SELECT COUNT(*) FROM Patients)*100, 0 ),
		'Норма'),

(ROUND((SELECT SUM (CASE WHEN BMI BETWEEN 25.1 AND 30 THEN 1.0 ELSE 0 END)FROM Patients) / ( SELECT COUNT(*) FROM Patients)*100, 0 ),
		'Избыточная масса тела (предожирение)'),

(ROUND((SELECT SUM (CASE WHEN BMI BETWEEN 30.1 AND 35 THEN 1.0 ELSE 0 END) FROM Patients)/ ( SELECT COUNT(*) FROM Patients)*100  , 0 ),
		'Ожирение 1 степени'),

(ROUND((SELECT SUM (CASE WHEN BMI BETWEEN 35.1 AND 40 THEN 1.0 ELSE 0 END) FROM Patients)/ ( SELECT COUNT(*) FROM Patients)*100 , 0 ),
		'Ожирение 2 степени'),

(ROUND((SELECT SUM (
CASE WHEN BMI BETWEEN 40.1 AND 100 THEN 1.0 ELSE 0 END) FROM Patients)/ ( SELECT COUNT(*) FROM Patients)*100, 0 ),
		'Ожирение 3 степени')

SELECT * FROM #Statistics ORDER BY _BMI DESC ;

DROP TABLE #Statistics 

