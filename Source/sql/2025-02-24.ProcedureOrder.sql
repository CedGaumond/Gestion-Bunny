CREATE OR REPLACE PROCEDURE mark_order_received(p_order_id INT)
LANGUAGE plpgsql
AS $$
BEGIN
    -- Mark the order as delivered
    UPDATE orders
    SET is_delivered = true
    WHERE id = p_order_id;

    -- Update ingredient quantities
    UPDATE ingredients
    SET quantity_remaining = quantity_remaining + (
        SELECT COALESCE(SUM(oi.quantity * i.quantity_per_delivery_unit), 0)
        FROM order_ingredients oi
        JOIN ingredients i ON oi.ingredient_id = i.id
        WHERE oi.order_id = p_order_id
        AND oi.ingredient_id = ingredients.id
    )
    WHERE id IN (
        SELECT ingredient_id
        FROM order_ingredients
        WHERE order_id = p_order_id
    );

    COMMIT;
END;
$$;