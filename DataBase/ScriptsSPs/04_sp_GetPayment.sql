CREATE PROCEDURE [payments].[sp_GetPayments]
    @PaymentId UNIQUEIDENTIFIER = NULL
    
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        PY.payment_id_ui,
        CM.name_vc,
        SP.name_vc,
        PY.amount_dc,
        PY.currency_type_ch,
        PY.status_vc,
        PY.user_registration_vc,
        PY.date_registration_dt,
        PY.user_modification_vc,
        PY.date_modification_dt,
        PY.state_bt
    FROM payments.PAYMENT PY
	INNER JOIN payments.CUSTOMER CM ON CM.customer_id_ui = PY.customer_id_ui
	INNER JOIN payments.SERVICEPROVIDER SP ON SP.provider_id_ui = PY.provider_id_ui
    WHERE
        (PY.payment_id_ui = @PaymentId)
        AND(PY.state_bt = 1)
    ORDER BY PY.date_registration_dt DESC;
END
GO