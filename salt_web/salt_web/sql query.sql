 SELECT 
 c.FirstName + ' ' + c.LastName as CollectionAssignedToName, c.userid,  *, 
 a.[FirstName]+' '+a.[LastName] AS AcctRep, b.[FirstName]+' '+b.LastName AS Consultant   
 FROM dbo.Clients  LEFT OUTER JOIN  
 dbo.aspnet_Users AS c ON dbo.Clients.CollectionsAssignedTo = c.UserId 
 LEFT OUTER JOIN dbo.aspnet_Users AS b ON dbo.Clients.Auditors_Key = b.UserId 
 LEFT OUTER JOIN dbo.aspnet_Users AS a ON dbo.Clients.AcctRep = a.UserId 
 WHERE dbo.Clients.ClientPKID = '6b15af46-2939-49e7-bdaa-005e6df119f1'

  select * from aspnet_Users
  where lastname like'%Lucas%'


  

  select * from aspnet_Users
  where userid ='11890A47-AAA2-4AD8-BC8C-BBFA535EB7B3'


  SELECT c.FirstName + ' ' + c.LastName as CollectionAssignedToName,  a.[FirstName]+' '+a.[LastName] AS AcctRep, b.[FirstName]+' '+b.LastName AS Consultant,*  
                                          FROM dbo.Clients LEFT OUTER JOIN 
                                          dbo.aspnet_Users AS c ON dbo.Clients.CollectionsAssignedTo = c.UserId LEFT OUTER JOIN
                                          dbo.aspnet_Users AS b ON dbo.Clients.Auditors_Key = b.UserId LEFT OUTER JOIN
                                          dbo.aspnet_Users AS a ON dbo.Clients.AcctRep = a.UserId 
                                          WHERE dbo.Clients.ClientPKID = '6b15af46-2939-49e7-bdaa-005e6df119f1'

 SELECT c.FirstName + ' ' + c.LastName as CollectionAssignedToName, a.[FirstName]+' '+a.[LastName] AS AcctRep, b.[FirstName]+' '+b.LastName AS Consultant, *  FROM dbo.Clients LEFT OUTER JOIN  dbo.aspnet_Users AS c ON dbo.Clients.CollectionsAssignedTo = c.UserId LEFT OUTER JOIN dbo.aspnet_Users AS b ON dbo.Clients.Auditors_Key = b.UserId LEFT OUTER JOIN dbo.aspnet_Users AS a ON dbo.Clients.AcctRep = a.UserId WHERE dbo.Clients.ClientPKID = '6b15af46-2939-49e7-bdaa-005e6df119f1'


  SELECT DISTINCT a.StateIdentified, b.fullname
  FROM RefundEntry a join menuusstates b on a.stateidentified=b.PKID 
  WHERE a.ClientPKID = '7e53f21f-0e94-4c46-a671-0055016bac64'
  ORDER BY b.fullname


  select * from vendors


SELECT a.pkid,a.LeftToRecover, b.companylegalname, 
a.AmountIdentified, c.abbrev, c.fullname, a.Adjusted, d.collectionnotename, 
a.amountrecovered, a.batchnumber, a.notes 
FROM RefundEntry a join Vendors b on b.vendorpkid=a.vendorpkid join menucollectionnote d
on a.collectionnote=d.collectionnotepkid join menuusstates c on a.stateidentified=c.pkid 
WHERE ClientPKID = '7e53f21f-0e94-4c46-a671-0055016bac64'
AND a.stateidentified = '633B4C2B-D4B3-433D-AF8A-61FABEFF6B22' 
ORDER BY b.companylegalname

SELECT SUM ([AmountIdentified]) FROM [refundentry] WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64')

SELECT (SELECT SUM([AmountRecovered])) / (SELECT (SELECT SUM([AmountIdentified])) + (SELECT SUM([Adjusted]))) * 100 FROM [refundentry] WHERE ([ClientPKID] ='7e53f21f-0e94-4c46-a671-0055016bac64')

SELECT (SELECT SUM([AmountRecovered]) / (SELECT SUM([AmountIdentified]))) * 100 FROM [refundentry] WHERE ([ClientPKID] ='7e53f21f-0e94-4c46-a671-0055016bac64')

SELECT (SUM([Adjusted])) + (SELECT SUM([AmountIdentified]))  FROM [refundentry] WHERE ([ClientPKID] ='7e53f21f-0e94-4c46-a671-0055016bac64')

SELECT sum([AmountRecovered]) FROM [refundentry] WHERE ([ClientPKID] ='7e53f21f-0e94-4c46-a671-0055016bac64')
SELECT sum([AmountIdentified]) FROM [refundentry] WHERE ([ClientPKID] ='7e53f21f-0e94-4c46-a671-0055016bac64')
SELECT sum([Adjusted]) FROM [refundentry] WHERE ([ClientPKID] ='7e53f21f-0e94-4c46-a671-0055016bac64')

SELECT Number FROM [CreditInterestReveived] WHERE [ClientPKID] ='7e53f21f-0e94-4c46-a671-0055016bac64'


IF(SELECT SUM([AmountIdentified])  + SUM([Adjusted]) FROM [refundentry] WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64')) = 0
    SELECT '0' 
ELSE
   select 'not zeror'

SELECT SUM([AmountRecovered]) / (SUM([AmountIdentified]) + SUM([Adjusted])) * 100 FROM [refundentry] WHERE [ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64'



SELECT (SELECT SUM([AmountIdentified])) + (SELECT SUM([Adjusted]))  FROM [refundentry] WHERE  ([ClientPKID] ='7e53f21f-0e94-4c46-a671-0055016bac64')
SELECT (SELECT SUM([AmountIdentified])) + (SELECT SUM([Adjusted]))  FROM [refundentry] WHERE  ([ClientPKID] ='7e53f21f-0e94-4c46-a671-0055016bac64')


SELECT (SELECT SUM([AmountRecovered])) / (SELECT (SELECT SUM([AmountIdentified])) + (SELECT SUM([Adjusted]))) * 100 FROM [refundentry] WHERE ([ClientPKID] ='7e53f21f-0e94-4c46-a671-0055016bac64')

SELECT (SELECT SUM([AmountRecovered])) / (SELECT (SELECT SUM([AmountIdentified])) + (SELECT SUM([Adjusted]))) * 100 FROM refundentry WHERE ClientPKID ='7e53f21f-0e94-4c46-a671-0055016bac64'


select * from menuusstates
where pkid = '633B4C2B-D4B3-433D-AF8A-61FABEFF6B22' 




  SELECT [refundentry].[pkid],[refundentry].[LeftToRecover], [vendors].[companylegalname], 
  convert(varchar, [refundentry].[AmountIdentified],1) as AmountIdentified, [menuusstates].[abbrev], 
  convert(varchar,([refundentry].[Adjusted]),1) as Adjusted, [menucollectionnote].[collectionnotename], 
  convert(varchar,[refundentry].[amountrecovered],1) as AmountRecovered, [refundentry].[batchnumber], [refundentry].[notes] 
  FROM [RefundEntry] join [Vendors] on [vendors].[vendorpkid]=[refundentry].[vendorpkid] join [menucollectionnote] 
  on [refundentry].[collectionnote]=[menucollectionnote].[collectionnotepkid] join [menuusstates] on [refundentry].[stateidentified]=[menuusstates].[pkid] 
  WHERE ([ClientPKID] = convert(uniqueidentifier,'7e53f21f-0e94-4c46-a671-0055016bac64')) 
  AND ([refundentry].[stateidentified] = convert(uniqueidentifier,'633B4C2B-D4B3-433D-AF8A-61FABEFF6B22')) 
  ORDER BY [vendors].[companylegalname]


  SELECT [refundentry].[pkid],[refundentry].[LeftToRecover], [vendors].[companylegalname], 
  convert(varchar, [refundentry].[AmountIdentified],1) as AmountIdentified, [menuusstates].[abbrev], 
  convert(varchar,([refundentry].[Adjusted]),1) as Adjusted, [menucollectionnote].[collectionnotename], 
  convert(varchar,[refundentry].[amountrecovered],1) as AmountRecovered, [refundentry].[batchnumber], [refundentry].[notes] 
  FROM [RefundEntry] join [Vendors] on [vendors].[vendorpkid]=[refundentry].[vendorpkid] join [menucollectionnote] 
  on [refundentry].[collectionnote]=[menucollectionnote].[collectionnotepkid] join [menuusstates] on [refundentry].[stateidentified]=[menuusstates].[pkid] 
  WHERE ([ClientPKID] = convert(uniqueidentifier,'7e53f21f-0e94-4c46-a671-0055016bac64')) 
  AND ([refundentry].[stateidentified] = convert(uniqueidentifier,'DEBBA870-E135-432E-ACED-5EACB3E6E915'))
  ORDER BY [vendors].[companylegalname]

  SELECT DISTINCT a.StateIdentified, b.fullname FROM RefundEntry a join menuusstates b on a.stateidentified=b.PKID WHERE ClientPKID = '7e53f21f-0e94-4c46-a671-0055016bac64' ORDER BY b.fullname


  --refund at the state
    SELECT * FROM [StateRefundEntry] WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64') ORDER BY BatchDescription ASC
  --total:
   --about submitted: 
   
  SELECT SUM([AmountSubmitted]) FROM [staterefundentry] WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64')
  SELECT SUM([AmountPaid]) FROM [staterefundentry] WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64')
  SELECT SUM([DeniedAdjusted]) FROM [staterefundentry] WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64')
  SELECT SUM([BalanceState]) FROM [staterefundentry] WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64')
  SELECT ISNULL((SELECT SUM([BalanceState]) FROM [staterefundentry] WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64')), 0)+
	(SELECT ISNULL(SUM([AmountRecovered]),0) FROM [refundentry] WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64'))


--future benefit
--grid
SELECT * FROM [FutureBenefitsEntry] WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64')  ORDER BY Installment ASC


SELECT (SELECT SUM(OriginalAmount) FROM futurebenefitsentry 
WHERE ClientPKID = '7e53f21f-0e94-4c46-a671-0055016bac64')

SELECT (SELECT SUM([AmountRecovered]) FROM [futurebenefitsentry] WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64'))
- (SELECT SUM([OriginalAmount]) FROM [futurebenefitsentry] WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64'))

SELECT (SELECT SUM([AmountRecovered]) FROM [futurebenefitsentry] WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64'))

--future balance

SELECT * FROM [CustomerPaymentsEntry] WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64') ORDER BY TimeStamp

SELECT [ClientPKID] FROM [Clients] 
WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64')


--customer payments

SELECT * FROM [CustomerPaymentsEntry] WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64')


SELECT SUM(PrepayAmount) - SUM(PrepayCredit), SUM([AppliedBalance])
FROM CustomerPaymentsEntry WHERE ClientPKID = '7e53f21f-0e94-4c46-a671-0055016bac64'



SELECT ISNULL((SELECT SUM(PrepayAmount) FROM CustomerPaymentsEntry WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64')), 0)
- ISNULL((SELECT SUM(PrepayCredit) FROM CustomerPaymentsEntry WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64')), 0)

SELECT SUM([AppliedBalance]) FROM [CustomerPaymentsEntry] WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64')


DECLARE @FeeTerm DECIMAL(18, 4) SELECT @FeeTerm = ISNULL(FeeTerms, 0) FROM Clients WHERE [ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64' SET @FeeTerm = ISNULL(@FeeTerm, 0) / 100 DECLARE @Total1 DECIMAL(18, 4) SELECT @Total1 =SUM([AmountRecovered]) FROM [refundentry] WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64') SET @Total1 = ISNULL(@Total1, 0) DECLARE @Total2 DECIMAL(18, 4) SELECT @Total2 = (CASE WHEN Number IS NULL OR Number = '' THEN '0.00' ELSE Number END) FROM [CreditInterestReveived] WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64') SET @Total2 = ISNULL(@Total2, 0) DECLARE @Total3 DECIMAL(18, 4) SELECT @Total3 = SUM([AmountRecovered]) FROM [futurebenefitsentry] WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64') SET @Total3 = ISNULL(@Total3, 0) DECLARE @Total4 DECIMAL(18, 4) SELECT @Total4 = SUM([AppliedBalance]) FROM [CustomerPaymentsEntry] WHERE ([ClientPKID] = convert(uniqueidentifier, '7e53f21f-0e94-4c46-a671-0055016bac64')) SET @Total4 = ISNULL(@Total4, 0) SELECT (@Total1 + @Total2 + @Total3) * @FeeTerm - @Total4 as tt






DECLARE @FeeTerm DECIMAL(18, 4)
SELECT @FeeTerm = ISNULL(FeeTerms, 0) FROM Clients WHERE [ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64'
SET @FeeTerm = ISNULL(@FeeTerm, 0) / 100
                                
DECLARE @Total1 DECIMAL(18, 4)
SELECT @Total1 =SUM([AmountRecovered]) FROM [refundentry] WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64')
SET @Total1 = ISNULL(@Total1, 0)
                                
DECLARE @Total2 DECIMAL(18, 4)
SELECT @Total2 = (CASE WHEN Number IS NULL OR Number = '' THEN '0.00' ELSE Number END) FROM [CreditInterestReveived] WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64')
SET @Total2 = ISNULL(@Total2, 0)
                                
DECLARE @Total3 DECIMAL(18, 4)
SELECT @Total3 = SUM([AmountRecovered]) FROM [futurebenefitsentry] WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64')
SET @Total3 = ISNULL(@Total3, 0)
                                
DECLARE @Total4 DECIMAL(18, 4)
SELECT @Total4 = SUM([AppliedBalance]) FROM [CustomerPaymentsEntry] WHERE ([ClientPKID] = convert(uniqueidentifier, '7e53f21f-0e94-4c46-a671-0055016bac64'))
SET @Total4 = ISNULL(@Total4, 0)
SELECT (@Total1 + @Total2 + @Total3) * @FeeTerm - @Total4


DECLARE @FeeTerm DECIMAL(18, 4)
SELECT @FeeTerm = ISNULL(FeeTerms, 0) FROM Clients WHERE [ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64'
SET @FeeTerm = ISNULL(@FeeTerm, 0) / 100
                                
DECLARE @Total1 DECIMAL(18, 4)
SET @Total1 = (SELECT ISNULL(SUM([BalanceState]),0) FROM [staterefundentry] 
WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64'))+(SELECT ISNULL(SUM([AmountRecovered]),0) FROM [refundentry] 
WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64'))+(SELECT ISNULL(SUM([AmountSubmitted]),0) FROM [staterefundentry] WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64'))
SET @Total1 = ISNULL(@Total1, 0)
                                
DECLARE @Total2 DECIMAL(18, 4)
SELECT @Total2 = (CASE WHEN Number IS NULL OR Number = '' THEN '0.00' ELSE Number END) FROM [CreditInterestReveived] WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64')
SET @Total2 = ISNULL(@Total2, 0)
                                
DECLARE @Total3 DECIMAL(18, 4)
SELECT @Total3 = SUM([AmountRecovered]) FROM [futurebenefitsentry] WHERE ([ClientPKID] = '7e53f21f-0e94-4c46-a671-0055016bac64')
SET @Total3 = ISNULL(@Total3, 0)
                                
DECLARE @Total4 DECIMAL(18, 4)
SELECT @Total4 = SUM([AppliedBalance]) FROM [CustomerPaymentsEntry] WHERE ([ClientPKID] = convert(uniqueidentifier, '7e53f21f-0e94-4c46-a671-0055016bac64'))
SET @Total4 = ISNULL(@Total4, 0)
                                
SELECT (@Total1 + @Total2 + @Total3) * @FeeTerm - @Total4

declare @ClientPKID varchar(60)
set @ClientPKID = '7e53f21f-0e94-4c46-a671-0055016bac64'
IF NOT EXISTS(SELECT TOP 1 1 FROM ProgressPayments WHERE [ClientPKID] = CONVERT(UNIQUEIDENTIFIER, @ClientPKID))
    INSERT INTO ProgressPayments(ClientPKID, ProgressPayment) VALUES (CONVERT(UNIQUEIDENTIFIER, @ClientPKID), '')
                                    
SELECT ProgressPayment FROM [ProgressPayments] WHERE [ClientPKID] = CONVERT(UNIQUEIDENTIFIER, @ClientPKID)


declare @ClientPKID varchar(60)
set @ClientPKID = '7e53f21f-0e94-4c46-a671-0055016bac64'

DECLARE @FeeTerm DECIMAL(18, 4)
SELECT @FeeTerm = ISNULL(FeeTerms, 0) FROM Clients WHERE [ClientPKID] = convert(uniqueidentifier,@ClientPKID)
SET @FeeTerm = ISNULL(@FeeTerm, 0) / 100
                                
DECLARE @Total1 DECIMAL(18, 4)
SELECT @Total1 =SUM([AmountRecovered]) FROM [refundentry] WHERE ([ClientPKID] = @ClientPKID)
SET @Total1 = ISNULL(@Total1, 0)
                                
DECLARE @Total2 DECIMAL(18, 4)
SELECT @Total2 = (CASE WHEN Number IS NULL OR Number = '' THEN '0.00' ELSE Number END) FROM [CreditInterestReveived] WHERE ([ClientPKID] = @ClientPKID)
SET @Total2 = ISNULL(@Total2, 0)
                                
DECLARE @Total3 DECIMAL(18, 4)
SELECT @Total3 = SUM([AmountRecovered]) FROM [futurebenefitsentry] WHERE ([ClientPKID] = @ClientPKID)
SET @Total3 = ISNULL(@Total3, 0)
                                
DECLARE @Total4 DECIMAL(18, 4)
SELECT @Total4 = SUM([AppliedBalance]) FROM [CustomerPaymentsEntry] WHERE ([ClientPKID] = convert(uniqueidentifier, @ClientPKID))
SET @Total4 = ISNULL(@Total4, 0)
                                
DECLARE @Total5 DECIMAL(18, 4)
SELECT @Total5 = SUM([PrepayAmount]) FROM [CustomerPaymentsEntry] WHERE ([ClientPKID] = convert(uniqueidentifier, @ClientPKID))
SET @Total5 = ISNULL(@Total5, 0)
                                
DECLARE @Total6 DECIMAL(18, 4)
SELECT @Total6 = [ProgressPayment] FROM [ProgressPayments] WHERE ([ClientPKID] = convert(uniqueidentifier, @ClientPKID))
SET @Total6 = ISNULL(@Total6, 0)
SELECT (@Total1 + @Total2 + @Total3) * @FeeTerm - @Total4 + @Total5 +@Total6

--quickbook update

IF NOT EXISTS(SELECT TOP 1 1 FROM ProgressPayments WHERE [ClientPKID] = CONVERT(UNIQUEIDENTIFIER, @ClientPKID))
    INSERT INTO ProgressPayments(ClientPKID, ProgressPayment) VALUES (CONVERT(UNIQUEIDENTIFIER, @ClientPKID), @ProgressPayment)
ELSE
    UPDATE ProgressPayments SET ProgressPayment = @ProgressPayment WHERE ClientPKID = CONVERT(UNIQUEIDENTIFIER, @ClientPKID)


--credit interest - update
declare @ClientPKID varchar(60)
set @ClientPKID = '7e53f21f-0e94-4c46-a671-0055016bac64'
SELECT * FROM CreditInterestReveived WHERE [ClientPKID] = CONVERT(UNIQUEIDENTIFIER, @ClientPKID)

IF NOT EXISTS(SELECT TOP 1 1 FROM CreditInterestReveived WHERE [ClientPKID] = @ClientPKID)
    INSERT INTO CreditInterestReveived(ClientPKID, Number) VALUES (@ClientPKID, @Number)
ELSE
    UPDATE CreditInterestReveived SET Number = @Number WHERE ClientPKID = @ClientPKID

--insert new
--state

SELECT [PKID], [FullName] FROM [MenuUSStates] ORDER BY [FullName]


declare @ClientPKID varchar(60)
set @ClientPKID = '7e53f21f-0e94-4c46-a671-0055016bac64'
SELECT [ClientPKID] FROM [Clients] WHERE ([ClientPKID] = @ClientPKID)

SELECT [VendorPKID], [CompanyLegalName] FROM [Vendors]
where  companylegalname <> '' and companylegalname is not null 
ORDER BY [CompanyLegalName]


--new

SELECT * FROM [MenuCollectionNote]

INSERT INTO [RefundEntry] ([ClientPKID], [VendorPKID], [AmountIdentified], [StateIdentified], [Adjusted], 
[CollectionNote], [AmountRecovered], [BatchNumber], [Notes], [LeftToRecover]) 
VALUES (convert(uniqueidentifier,@ClientPKID), convert(uniqueidentifier, @VendorPKID), @AmountIdentified, 
convert(uniqueidentifier, @StateIdentified), 0, convert(uniqueidentifier, @CollectionNote), 0, @BatchNumber, @Notes, @AmountIdentified)

sp_help refundentry

select * from refundentry
where clientpkid = '6a7eb9b0-50b7-48e4-89ce-00150bdf0e3b'

SELECT DISTINCT a.StateIdentified, b.fullname FROM RefundEntry
 a join menuusstates b on a.stateidentified=b.PKID WHERE ClientPKID = 'ce723426-bfbe-4c48-980a-f5181870d0b4' ORDER BY b.fullname

   
select * from refundentry
where clientpkid = 'ce723426-bfbe-4c48-980a-f5181870d0b4'

go
select * from CreditInterestReveived
where clientpkid = 'ce723426-bfbe-4c48-980a-f5181870d0b4'


update CreditInterestReveived
set number = '92.02'
where clientpkid = 'ce723426-bfbe-4c48-980a-f5181870d0b4'
 
declare @ClientPKID varchar(60)
set @ClientPKID = 'ce723426-bfbe-4c48-980a-f5181870d0b4'

SELECT [refundentry].[pkid],[refundentry].[LeftToRecover], [vendors].[companylegalname], 
convert(varchar, [refundentry].[AmountIdentified],1) as AmountIdentified, [menuusstates].[abbrev], 
convert(varchar,([refundentry].[Adjusted]),1) as Adjusted, [menucollectionnote].[collectionnotename], 
convert(varchar,[refundentry].[amountrecovered],1) as AmountRecovered, [refundentry].[batchnumber], [refundentry].[notes] 
FROM [RefundEntry] join [Vendors] on [vendors].[vendorpkid]=[refundentry].[vendorpkid] 
join [menucollectionnote] on [refundentry].[collectionnote]=[menucollectionnote].[collectionnotepkid] 
join [menuusstates] on [refundentry].[stateidentified]=[menuusstates].[pkid] 
WHERE ([ClientPKID] = convert(uniqueidentifier,'ce723426-bfbe-4c48-980a-f5181870d0b4')) 
AND ([refundentry].[stateidentified] = convert(uniqueidentifier,'69661623-F106-40C3-826C-B28126885C85')) 
ORDER BY [vendors].[companylegalname]

SELECT        TOP (100) PERCENT dbo.RefundEntry.PKID, dbo.RefundEntry.LeftToRecover, dbo.Vendors.CompanyLegalName, CONVERT(varchar, 
				dbo.RefundEntry.AmountIdentified, 1) AS AmountIdentified, dbo.MenuUSStates.Abbrev, CONVERT(varchar, dbo.RefundEntry.Adjusted, 1) AS Adjusted, 
				dbo.MenuCollectionNote.CollectionNoteName, CONVERT(varchar, dbo.RefundEntry.AmountRecovered, 1) AS AmountRecovered, dbo.RefundEntry.BatchNumber, 
				dbo.RefundEntry.Notes
FROM            dbo.RefundEntry INNER JOIN
                dbo.MenuUSStates ON dbo.RefundEntry.StateIdentified = dbo.MenuUSStates.PKID INNER JOIN
                dbo.Vendors ON dbo.Vendors.VendorPKID = dbo.RefundEntry.VendorPKID LEFT OUTER JOIN
                dbo.MenuCollectionNote ON dbo.RefundEntry.CollectionNote = dbo.MenuCollectionNote.CollectionNotePKID
WHERE        (dbo.RefundEntry.ClientPKID = CONVERT(uniqueidentifier, 'ce723426-bfbe-4c48-980a-f5181870d0b4')) AND 
            (dbo.RefundEntry.StateIdentified = CONVERT(uniqueidentifier, '69661623-F106-40C3-826C-B28126885C85'))
ORDER BY dbo.Vendors.CompanyLegalName



SELECT DISTINCT[refundentry].[StateIdentified], [menuusstates].[fullname] 
FROM [RefundEntry] join [menuusstates] on [refundentry].[stateidentified]=[menuusstates].[PKID] 
WHERE ([ClientPKID] = convert(uniqueidentifier,@ClientPKID)) ORDER BY [menuusstates].[fullname]


