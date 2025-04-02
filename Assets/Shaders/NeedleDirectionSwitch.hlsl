void SelectDirection_float(float state, out float3 New)
{
    if (state == 0) {
        New = float3(0.0, -0.025, 0.0); // dol
    } else if (state == 1) {
        New = float3(0.0, 0.025, 0.0);  // gora
    } else if (state == 2) {
        New = float3(0.025, 0, 0.0); // lewo
    } else if (state == 3) {
        New = float3(-0.025, 0, 0.0);  // prawo
    } else {
        New = float3(0.0, 0.0, 0.0);  // fallback
    }
}
