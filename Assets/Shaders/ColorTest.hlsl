void SelectColor_float(float state, out float4 color)
{
    if (state == 0) {
        color = float4(1, 0, 0, 1); // Czerwony
    } else if (state == 1) {
        color = float4(0, 1, 0, 1); // Zielony
    } else if (state == 2) {
        color = float4(0, 0, 1, 1); // Niebieski
    } else if (state == 3) {
        color = float4(1, 1, 1, 1); // Bialy
    } else {
        color = float4(0, 0, 0, 1); // Czarny fallback
    }
}