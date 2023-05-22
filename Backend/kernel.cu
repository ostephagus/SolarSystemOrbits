#include "cuda_runtime.h"
#include "device_launch_parameters.h"
#include <stdio.h>
#include <iostream>
#define _USE_MATH_DEFINES
#include <math.h>;
using namespace std;

float* __declspec(dllexport) challengeOne();//non cuda

float* __declspec(dllexport) challengeTwo();// non cuda

float* __declspec(dllexport) challengeThree(); //

float* __declspec(dllexport) challengeFour(); //can be cuda as rotation matrix

float* __declspec(dllexport) challengeFive(); //can be cuda as lots of samples

float* __declspec(dllexport) challengeSix(); //can be cuda as image

float* __declspec(dllexport) challengeSeven(); //can be cuda as image data

float radius(float semimaj, float e, float theta) {
    return (semimaj * (1 - powf(e, 2))) / (1 - cosf(theta));
}

float* challengeOne() {
    float radii[8] = { 0.387, 0.723, 1, 1.523, 5.2, 9.58, 19.29, 30.25 };
    float periods[8] = { 0.24, 0.62, 1, 1.88, 11.86, 29.63, 84.75, 166.34 };
    float mean = 0;
    float r2;
    for (int i = 0; i < 8; i++) {
        mean += periods[i] / 8.0f;
    }
    float rss=0;
    float css = 0;
    for (int i = 0; i < 8; i++) {
        rss += powf(powf(periods[i]- sqrt(radii[i]), 3), 2);
        css += powf(periods[i] - mean, 2);
    }

    float* d_out = (float*)malloc(sizeof(float) * 17);
    for (int i = 0; i < 8; i++) {
        d_out[i] = radii[i];
    }
    for (int i = 8; i < 16; i++) {
        d_out[i] = periods[i%8];
    }
    r2 = 1 - (rss / css);
    d_out[16] = r2;
    return d_out;
}

float* challengeTwo(float semimaj, float e) {
    float dtheta = 0.002 * M_PI;
    float ttheta = 2 * M_PI;

    
    float theta = 0;
    float rad;
    float* dout = (float*)malloc(4000 * sizeof(float));
    for (int i = 0; i < 2000; i++) {
        theta += dtheta;
        rad = radius(semimaj, e, theta);
        dout[i] = rad * cosf(theta);
        dout[i+1000] = rad * sinf(theta);
    }
    return dout;

}