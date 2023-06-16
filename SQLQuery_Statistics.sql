--��������� ������� ��� ����������� ���������� � ���������� �� ��������
if not exists (select * from sysobjects where name='#Statistics')
create table #Statistics (
BMI float(24),
Descript NVARCHAR(60)
    )

DECLARE  @row INT,
@minBMI float(24), @maxBMI float(24); 

SET @row = 1;

WHILE @row <= (SELECT COUNT(*) FROM IndicatorsBMI)
    BEGIN

	--����������� � ������������ ��� 
	--� ������������ � ����� ( @row ) �� ������� � �������������� ���
	SET @minBMI =( SELECT  MinBMI FROM IndicatorsBMI WHERE ID = @row); 
	SET @maxBMI =( SELECT  MaxBMI FROM IndicatorsBMI WHERE ID = @row);

	INSERT INTO #Statistics (#Statistics.BMI, #Statistics.Descript) VALUES 
	( 
		--���������� �������� �� ������ ���������� 
		((SELECT SUM (CASE WHEN BMI BETWEEN @minBMI AND @maxBMI THEN 1.0 ELSE 0 END) FROM Patients)/( SELECT COUNT(*) FROM Patients )*100),
		--�������� ��� ����������� ��� ���������������� ���������� 
		--� ������������ � ����� ( @row ) �� ������� � �������������� ���
		(SELECT Description FROM IndicatorsBMI WHERE ID = @row )
	)

	SET @row = @row + 1;
END;

SELECT * FROM #Statistics ORDER BY #Statistics.BMI DESC ;

DROP TABLE #Statistics 

