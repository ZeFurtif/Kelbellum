#ifdef SHADERGRAPH_PREVIEW
// In Shader Graph Preview we will assume a default light direction and white color
direction = half3(-0.3, -0.8, 0.6);
color = half3(1, 1, 1);
distanceAttenuation = 1.0;
shadowAttenuation = 1.0;
#else
// Universal Render Pipeline
#if defined(UNIVERSAL_LIGHTING_INCLUDED)
Light mainLight = GetMainLight();
direction = -mainLight.direction;
color = mainLight.color;
distanceAttenuation = mainLight.distanceAttenuation;
shadowAttenuation = mainLight.shadowAttenuation;
#endif
#endif