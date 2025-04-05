void SelectNeedleDirection_float(float state, out float3 New)
{
    switch ((int)state)
    {
        case 0: New = float3(0.0, -0.025, 0.0); break; // down
        case 1: New = float3(0.0, 0.025, 0.0); break;  // up
        case 2: New = float3(0.025, 0.0, 0.0); break;  // left
        case 3: New = float3(-0.025, 0.0, 0.0); break; // right
        default: New = float3(0.0, 0.0, 0.0); break;
    }
}