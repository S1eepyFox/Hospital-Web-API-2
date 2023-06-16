USE Hospital;
if not exists (select * from sysobjects where name='#AgeGroupBMI')
CREATE TABLE #AgeGroupBMI (
    ID_AgeGroupBMI INT IDENTITY PRIMARY KEY,
    ID_AgeIntervals INT FOREIGN KEY REFERENCES ID_IntervalsAge(ID_IntervalsAge),
	PercentPatients float(24) ,
	Description nchar(50) 
)

--Таблица для одной возрастной группы 
if not exists (select * from sysobjects where name='#Age_BMI')
CREATE TABLE #_Age_BMI (
    _Age INT ,
	_BMI float(24)
)


DECLARE @col INT, @row INT,
@minAge INT, @maxAge INT,
@minBMI float(24), @maxBMI float(24);

SET @col = 1; 
SET @row = 1; 

WHILE @col <= 10
    BEGIN

		SET @minAge =( SELECT  MinAge FROM IntervalsAge WHERE ID_IntervalsAge = @col);
		SET @maxAge =( SELECT  MaxAge FROM IntervalsAge WHERE ID_IntervalsAge = @col);

		INSERT INTO #_Age_BMI (_Age, _BMI)
		SELECT age, BMI FROM Patients  WHERE  age BETWEEN @minAge AND @maxAge 
		
			
		WHILE @row <= 7
			BEGIN
				
				--Минимальный и максимальный ИМТ 
				--в соответствии с рядом ( @row ) из таблицы с рекомендациями ВОЗ
				SET @minBMI =( SELECT  MinBMI FROM IndicatorsBMI WHERE ID = @row);
				SET @maxBMI =( SELECT  MaxBMI FROM IndicatorsBMI WHERE ID = @row);

				INSERT INTO #AgeGroupBMI (ID_AgeIntervals,PercentPatients,Description)
				VALUES (
				--ID
				@col
				,
				--Вычисление процента от общего количества 
				(SELECT SUM (CASE WHEN _BMI BETWEEN @minBMI AND @maxBMI THEN 1.0 ELSE 0 END )FROM #_Age_BMI )/* Количество человек из первой категории */
				/( SELECT COUNT(*) FROM #_Age_BMI )*100 
				, 		
				--Описание для показателей ИМТ соответствующего промежутка 
				--в соответствии с рядом ( @row ) из таблицы с рекомендациями ВОЗ
				(SELECT  Description FROM IndicatorsBMI WHERE ID = @row  ));


			SET @row = @row + 1
			END;

	SET @row =  1 --Очистка счета  

	DELETE FROM #_Age_BMI --Очистка таблицы  

SET @col = @col + 1
END;

SELECT IntervalsAge.ID_IntervalsAge, IntervalsAge.MinAge, IntervalsAge.MaxAge, #AgeGroupBMI.PercentPatients, #AgeGroupBMI.Description
FROM IntervalsAge 
JOIN #AgeGroupBMI ON IntervalsAge.ID_IntervalsAge = #AgeGroupBMI.ID_AgeIntervals   ORDER BY MinAge ASC , PercentPatients DESC;
;

DROP TABLE #_Age_BMI 
DROP TABLE #AgeGroupBMI 
 
