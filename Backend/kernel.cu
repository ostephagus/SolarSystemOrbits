#include "cuda_runtime.h"
#include "device_launch_parameters.h"
#include <stdio.h>

#ifdef __cplusplus
extern "C" {
#endif
    float* __declspec(dllexport) challengeOne();//non cuda
#ifdef __cplusplus
}
#endif // __cplusplus

#ifdef __cplusplus
extern "C" {
#endif
    float* __declspec(dllexport) challengeTwo();// non cuda
#ifdef __cplusplus
}
#endif // __cplusplus

#ifdef __cplusplus
extern "C" {
#endif
    float* __declspec(dllexport) challengeThree(); //
#ifdef __cplusplus
}
#endif // __cplusplus

#ifdef __cplusplus
extern "C" {
#endif
    float* __declspec(dllexport) challengeFour(); //can be cuda as rotation matrix
#ifdef __cplusplus
}
#endif // __cplusplus

#ifdef __cplusplus
extern "C" {
#endif
    float* __declspec(dllexport) challengeFive(); //can be cuda as lots of samples
#ifdef __cplusplus
}
#endif // __cplusplus

#ifdef __cplusplus
extern "C" {
#endif
    float* __declspec(dllexport) challengeSix(); //can be cuda as image
#ifdef __cplusplus
}
#endif // __cplusplus

#ifdef __cplusplus
extern "C" {
#endif
    float* __declspec(dllexport) challengeSeven(); //can be cuda as image data
#ifdef __cplusplus
}
#endif // __cplusplus

float* challengeOne() {

}

