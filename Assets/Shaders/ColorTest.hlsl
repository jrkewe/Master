float4 GetColorByState_color(int state)
{
    switch (state)
    {
        case 0: return float4(1, 0, 0, 1); // Red
        case 1: return float4(0, 1, 0, 1); // Green
        case 2: return float4(0, 0, 1, 1); // Blue
        case 3: return float4(1, 1, 1, 1); // White
        default: return float4(0, 0, 0, 1); // Black (fallback)
    }
}

void SelectColor_float(float state, out float4 color)
{
    color = GetColorByState((int)state);
}