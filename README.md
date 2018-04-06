# GalacticMotions

> This project is based on the paper arXiv:1211.4045. The main idea is to visualize the dynamical behavior of astrophysical objects 
(e.g. galaxies) taking into account both the gravitational attraction between them and the cosmological expansion of the Universe

## Table of Contents

- [Equations of motion](#equations-of-motion)
- [Modelling/Simulation idea](#modellingsimulation-idea)
- [Numerical Integration](#numerical-integration)
- [Examples](#examples)

## Equations of motion

> Here I rewrite the equations of motion  for the appropriate numerical modelling (please, refer to the arXiv:1211.4045 for complete theoretical description). 

A system of equations describing the motion of a system of N point particles:

![equation](http://latex.codecogs.com/gif.latex?-%5Cfrac%7B1%7D%7B%5Cbar%7Bm%7D%7D%5Csum_%7Bj%5Cneq%20i%7D%5Cfrac%7Bm_j%5Cleft%28%5Ctilde%7BX%7D_i-%5Ctilde%7BX%7D_j%20%5Cright%29%7D%7B%5Cleft%5B%5Cleft%28%5Ctilde%7BX%7D_i-%5Ctilde%7BX%7D_j%20%5Cright%20%29%5E2-%5Cleft%28%20%5Ctilde%7BY%7D_i-%5Ctilde%7BY%7D_j%20%5Cright%20%29%5E2-%5Cleft%28%5Ctilde%7BZ%7D_i-%5Ctilde%7BZ%7D_j%20%5Cright%29%5E2%20%5Cright%20%5D%5E%7B3/2%7D%7D%3D%5Cfrac%7B1%7D%7B%5Ctilde%7Ba%7D%7D%5Cleft%28%5Cfrac%7Bd%5E2%20%5Ctilde%7BX%7D_i%7D%7Bd%5Ctilde%7Bt%7D%5E2%7D%5Ctilde%7Ba%7D-%5Cfrac%7Bd%5E2%5Ctilde%7Ba%7D%7D%7Bd%5Ctilde%7Bt%7D%5E2%7D%5Ctilde%7BX%7D_i%5Cright%29%2C%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%281%29)

![equation](http://latex.codecogs.com/gif.latex?-%5Cfrac%7B1%7D%7B%5Cbar%7Bm%7D%7D%5Csum_%7Bj%5Cneq%20i%7D%5Cfrac%7Bm_j%5Cleft%28%5Ctilde%7BY%7D_i-%5Ctilde%7BY%7D_j%20%5Cright%29%7D%7B%5Cleft%5B%5Cleft%28%5Ctilde%7BX%7D_i-%5Ctilde%7BX%7D_j%20%5Cright%20%29%5E2-%5Cleft%28%20%5Ctilde%7BY%7D_i-%5Ctilde%7BY%7D_j%20%5Cright%20%29%5E2-%5Cleft%28%5Ctilde%7BZ%7D_i-%5Ctilde%7BZ%7D_j%20%5Cright%29%5E2%20%5Cright%20%5D%5E%7B3/2%7D%7D%3D%5Cfrac%7B1%7D%7B%5Ctilde%7Ba%7D%7D%5Cleft%28%5Cfrac%7Bd%5E2%20%5Ctilde%7BY%7D_i%7D%7Bd%5Ctilde%7Bt%7D%5E2%7D%5Ctilde%7Ba%7D-%5Cfrac%7Bd%5E2%5Ctilde%7Ba%7D%7D%7Bd%5Ctilde%7Bt%7D%5E2%7D%5Ctilde%7BY%7D_i%5Cright%29%2C%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%282%29)

![equation](http://latex.codecogs.com/gif.latex?-%5Cfrac%7B1%7D%7B%5Cbar%7Bm%7D%7D%5Csum_%7Bj%5Cneq%20i%7D%5Cfrac%7Bm_j%5Cleft%28%5Ctilde%7BZ%7D_i-%5Ctilde%7BZ%7D_j%20%5Cright%29%7D%7B%5Cleft%5B%5Cleft%28%5Ctilde%7BX%7D_i-%5Ctilde%7BX%7D_j%20%5Cright%20%29%5E2-%5Cleft%28%20%5Ctilde%7BY%7D_i-%5Ctilde%7BY%7D_j%20%5Cright%20%29%5E2-%5Cleft%28%5Ctilde%7BZ%7D_i-%5Ctilde%7BZ%7D_j%20%5Cright%29%5E2%20%5Cright%20%5D%5E%7B3/2%7D%7D%3D%5Cfrac%7B1%7D%7B%5Ctilde%7Ba%7D%7D%5Cleft%28%5Cfrac%7Bd%5E2%20%5Ctilde%7BZ%7D_i%7D%7Bd%5Ctilde%7Bt%7D%5E2%7D%5Ctilde%7Ba%7D-%5Cfrac%7Bd%5E2%5Ctilde%7Ba%7D%7D%7Bd%5Ctilde%7Bt%7D%5E2%7D%5Ctilde%7BZ%7D_i%5Cright%29%2C%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%283%29)

Here ![equation](http://latex.codecogs.com/gif.latex?%5Ctilde%7BX%7D_i%2C%20%5Ctilde%7BY%7D_i%2C%20%5Ctilde%7BZ%7D_i) are the functions of the normalized time ![equation](http://latex.codecogs.com/gif.latex?%5Ctilde%7Bt%7D), i.e. normalized "physical" Cartesian coordinates of the i-th particle (![equation](http://latex.codecogs.com/gif.latex?i%3D1%2C2%2C...%2CN)), ![equation](http://latex.codecogs.com/gif.latex?m_i) its mass, ![equation](http://latex.codecogs.com/gif.latex?%5Cbar%7Bm%7D%3D%5Cleft%28m_1&plus;m_2&plus;...&plus;m_N%5Cright%29/N) is the average mass of all particles,
![equation](http://latex.codecogs.com/gif.latex?%5Ctilde%7Ba%7D) is the function of normalized time ![equation](http://latex.codecogs.com/gif.latex?%5Ctilde%7Bt%7D), i.e. normalized scale factor:

![equation](http://latex.codecogs.com/gif.latex?%5Ctilde%7Ba%7D%3D%5Cleft%28%5Cfrac%7B%5COmega_M%7D%7B%5COmega_%5CLambda%7D%20%5Cright%20%29%5E%7B1/3%7D%5Cleft%5B%5Cleft%281&plus;%5Cfrac%7B%5COmega_%5CLambda%7D%7B%5COmega_M%7D%5Cright%29%5E%7B1/2%7D%5Csinh%5Cleft%28%5Cfrac%7B3%7D%7B2%7D%5COmega_%5CLambda%5E%7B1/2%7D%5Ctilde%7Bt%7D%5Cright%29&plus;%5Cleft%28%5Cfrac%7B%5COmega_%5CLambda%7D%7B%5COmega_M%7D%5Cright%20%29%5E%7B1/2%7D%5Ccosh%5Cleft%28%5Cfrac%7B3%7D%7B2%7D%5COmega_%5CLambda%5E%7B1/2%7D%5Ctilde%7Bt%7D%5Cright%29%5Cright%20%5D%5E%7B2/3%7D%2C%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%284%29)

where ![equation](http://latex.codecogs.com/gif.latex?%5COmega_M%5Capprox0.31) and ![equation](http://latex.codecogs.com/gif.latex?%5COmega_%5CLambda%5Capprox0.69). Present values are ![equation](http://latex.codecogs.com/gif.latex?%5Ctilde%7Bt%7D%3D0), ![equation](http://latex.codecogs.com/gif.latex?%5Ctilde%7Ba%7D%3D1).
Let's implement the transformation ![equation](http://latex.codecogs.com/gif.latex?%5Ctilde%7BX%7D_i%3D%5Ctilde%7Ba%7D%5Ctilde%7Bx%7D_i%2C%20%5Cquad%20%5Ctilde%7BY%7D_i%3D%5Ctilde%7Ba%7D%5Ctilde%7By%7D_i%2C%20%5Cquad%20%5Ctilde%7BZ%7D_i%3D%5Ctilde%7Ba%7D%5Ctilde%7Bz%7D_i) (with ![equation](http://latex.codecogs.com/gif.latex?%5Ctilde%7Bx%7D%3Da_0x%5Cleft%5BH_0%5E2/%5Cleft%28G_N%20%5Cbar%7Bm%7D%5Cright%29%5Cright%5D%5E%7B1/3%7D)). Then,

![equation](http://latex.codecogs.com/gif.latex?-%5Cfrac%7B1%7D%7B%5Cbar%7Bm%7D%5Ctilde%7Ba%7D%5E3%7D%5Csum_%7Bj%5Cneq%20i%7D%5Cfrac%7Bm_j%5Cleft%28%5Ctilde%7Bx%7D_i-%5Ctilde%7Bx%7D_j%20%5Cright%29%7D%7B%5Cleft%5B%5Cleft%28%5Ctilde%7Bx%7D_i-%5Ctilde%7Bx%7D_j%20%5Cright%20%29%5E2&plus;%5Cleft%28%20%5Ctilde%7By%7D_i-%5Ctilde%7By%7D_j%20%5Cright%20%29%5E2&plus;%5Cleft%28%5Ctilde%7Bz%7D_i-%5Ctilde%7Bz%7D_j%20%5Cright%29%5E2%20%5Cright%20%5D%5E%7B3/2%7D%7D%3D%5Cfrac%7Bd%5E2%20%5Ctilde%7Bx%7D_i%7D%7Bd%5Ctilde%7Bt%7D%5E2%7D&plus;%5Cfrac%7B2%7D%7B%5Ctilde%7Ba%7D%7D%5Cfrac%7Bd%5Ctilde%7Ba%7D%7D%7Bd%5Ctilde%7Bt%7D%7D%5Cfrac%7Bd%5Ctilde%7Bx%7D_i%7D%7Bd%5Ctilde%7Bt%7D%7D%2C%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%285%29)

![equation](http://latex.codecogs.com/gif.latex?-%5Cfrac%7B1%7D%7B%5Cbar%7Bm%7D%5Ctilde%7Ba%7D%5E3%7D%5Csum_%7Bj%5Cneq%20i%7D%5Cfrac%7Bm_j%5Cleft%28%5Ctilde%7By%7D_i-%5Ctilde%7By%7D_j%20%5Cright%29%7D%7B%5Cleft%5B%5Cleft%28%5Ctilde%7Bx%7D_i-%5Ctilde%7Bx%7D_j%20%5Cright%20%29%5E2&plus;%5Cleft%28%20%5Ctilde%7By%7D_i-%5Ctilde%7By%7D_j%20%5Cright%20%29%5E2&plus;%5Cleft%28%5Ctilde%7Bz%7D_i-%5Ctilde%7Bz%7D_j%20%5Cright%29%5E2%20%5Cright%20%5D%5E%7B3/2%7D%7D%3D%5Cfrac%7Bd%5E2%20%5Ctilde%7By%7D_i%7D%7Bd%5Ctilde%7Bt%7D%5E2%7D&plus;%5Cfrac%7B2%7D%7B%5Ctilde%7Ba%7D%7D%5Cfrac%7Bd%5Ctilde%7Ba%7D%7D%7Bd%5Ctilde%7Bt%7D%7D%5Cfrac%7Bd%5Ctilde%7By%7D_i%7D%7Bd%5Ctilde%7Bt%7D%7D%2C%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%286%29)

![equation](http://latex.codecogs.com/gif.latex?-%5Cfrac%7B1%7D%7B%5Cbar%7Bm%7D%5Ctilde%7Ba%7D%5E3%7D%5Csum_%7Bj%5Cneq%20i%7D%5Cfrac%7Bm_j%5Cleft%28%5Ctilde%7Bz%7D_i-%5Ctilde%7Bz%7D_j%20%5Cright%29%7D%7B%5Cleft%5B%5Cleft%28%5Ctilde%7Bx%7D_i-%5Ctilde%7Bx%7D_j%20%5Cright%20%29%5E2&plus;%5Cleft%28%20%5Ctilde%7By%7D_i-%5Ctilde%7By%7D_j%20%5Cright%20%29%5E2&plus;%5Cleft%28%5Ctilde%7Bz%7D_i-%5Ctilde%7Bz%7D_j%20%5Cright%29%5E2%20%5Cright%20%5D%5E%7B3/2%7D%7D%3D%5Cfrac%7Bd%5E2%20%5Ctilde%7Bz%7D_i%7D%7Bd%5Ctilde%7Bt%7D%5E2%7D&plus;%5Cfrac%7B2%7D%7B%5Ctilde%7Ba%7D%7D%5Cfrac%7Bd%5Ctilde%7Ba%7D%7D%7Bd%5Ctilde%7Bt%7D%7D%5Cfrac%7Bd%5Ctilde%7Bz%7D_i%7D%7Bd%5Ctilde%7Bt%7D%7D%2C%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%287%29)

Further transition to the "not-tilde" comoving coordinates xi, yi, zi gives

![equation](http://latex.codecogs.com/gif.latex?-%5Cfrac%7B1%7D%7B%5Cbar%7Bm%7D%5Ctilde%7Ba%7D%5E3%7D%5Csum_%7Bj%5Cneq%20i%7D%5Cfrac%7Bm_j%5Cleft%28x_i-x_j%20%5Cright%29%7D%7B%5Cleft%5B%5Cleft%28x_i-x_j%5Cright%29%5E2&plus;%5Cleft%28y_i-y_j%5Cright%20%29%5E2&plus;%5Cleft%28z_i-z_j%20%5Cright%29%5E2%20%5Cright%20%5D%5E%7B3/2%7D%7D%3D%5Cleft%28%5Cfrac%7Bd%5E2x_i%7D%7Bd%5Ctilde%7Bt%7D%5E2%7D&plus;%5Cfrac%7B2%7D%7B%5Ctilde%7Ba%7D%7D%5Cfrac%7Bd%5Ctilde%7Ba%7D%7D%7Bd%5Ctilde%7Bt%7D%7D%5Cfrac%7Bdx_i%7D%7Bd%5Ctilde%7Bt%7D%7D%5Cright%29a_0%5E3%5Cleft%28%5Cfrac%7BH_0%5E2%7D%7BG_N%5Cbar%7Bm%7D%7D%20%5Cright%20%29%2C%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%288%29)

![equation](http://latex.codecogs.com/gif.latex?-%5Cfrac%7B1%7D%7B%5Cbar%7Bm%7D%5Ctilde%7Ba%7D%5E3%7D%5Csum_%7Bj%5Cneq%20i%7D%5Cfrac%7Bm_j%5Cleft%28y_i-y_j%20%5Cright%29%7D%7B%5Cleft%5B%5Cleft%28x_i-x_j%5Cright%29%5E2&plus;%5Cleft%28y_i-y_j%5Cright%20%29%5E2&plus;%5Cleft%28z_i-z_j%20%5Cright%29%5E2%20%5Cright%20%5D%5E%7B3/2%7D%7D%3D%5Cleft%28%5Cfrac%7Bd%5E2y_i%7D%7Bd%5Ctilde%7Bt%7D%5E2%7D&plus;%5Cfrac%7B2%7D%7B%5Ctilde%7Ba%7D%7D%5Cfrac%7Bd%5Ctilde%7Ba%7D%7D%7Bd%5Ctilde%7Bt%7D%7D%5Cfrac%7Bdy_i%7D%7Bd%5Ctilde%7Bt%7D%7D%5Cright%29a_0%5E3%5Cleft%28%5Cfrac%7BH_0%5E2%7D%7BG_N%5Cbar%7Bm%7D%7D%20%5Cright%20%29%2C%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%289%29)

![equation](http://latex.codecogs.com/gif.latex?-%5Cfrac%7B1%7D%7B%5Cbar%7Bm%7D%5Ctilde%7Ba%7D%5E3%7D%5Csum_%7Bj%5Cneq%20i%7D%5Cfrac%7Bm_j%5Cleft%28z_i-z_j%20%5Cright%29%7D%7B%5Cleft%5B%5Cleft%28x_i-x_j%5Cright%29%5E2&plus;%5Cleft%28y_i-y_j%5Cright%20%29%5E2&plus;%5Cleft%28z_i-z_j%20%5Cright%29%5E2%20%5Cright%20%5D%5E%7B3/2%7D%7D%3D%5Cleft%28%5Cfrac%7Bd%5E2z_i%7D%7Bd%5Ctilde%7Bt%7D%5E2%7D&plus;%5Cfrac%7B2%7D%7B%5Ctilde%7Ba%7D%7D%5Cfrac%7Bd%5Ctilde%7Ba%7D%7D%7Bd%5Ctilde%7Bt%7D%7D%5Cfrac%7Bdz_i%7D%7Bd%5Ctilde%7Bt%7D%7D%5Cright%29a_0%5E3%5Cleft%28%5Cfrac%7BH_0%5E2%7D%7BG_N%5Cbar%7Bm%7D%7D%20%5Cright%20%29%2C%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%2810%29)

Finally, the transition to the comoving coordinates "with caps" (![equation](http://latex.codecogs.com/gif.latex?%5Chat%7Bx%7D%3Dx/l)) yields

![equation](http://latex.codecogs.com/gif.latex?-%5Cfrac%7B1%7D%7B%5Cbar%7Bm%7D%5Ctilde%7Ba%7D%5E3%7D%5Csum_%7Bj%5Cneq%20i%7D%5Cfrac%7Bm_j%5Cleft%28%5Chat%7Bx%7D_i-%5Chat%7Bx%7D_j%20%5Cright%29%7D%7B%5Cleft%5B%5Cleft%28%5Chat%7Bx%7D_i-%5Chat%7Bx%7D_j%20%5Cright%20%29%5E2&plus;%5Cleft%28%20%5Chat%7By%7D_i-%5Chat%7By%7D_j%20%5Cright%20%29%5E2&plus;%5Cleft%28%5Chat%7Bz%7D_i-%5Chat%7Bz%7D_j%20%5Cright%29%5E2%20%5Cright%20%5D%5E%7B3/2%7D%7D%3D%5Cleft%28%5Cfrac%7Bd%5E2%20%5Chat%7Bx%7D_i%7D%7Bd%5Ctilde%7Bt%7D%5E2%7D&plus;%5Cfrac%7B2%7D%7B%5Ctilde%7Ba%7D%7D%5Cfrac%7Bd%5Ctilde%7Ba%7D%7D%7Bd%5Ctilde%7Bt%7D%7D%5Cfrac%7Bd%5Chat%7Bx%7D_i%7D%7Bd%5Ctilde%7Bt%7D%7D%5Cright%29l%5E3a_0%5E3%5Cleft%28%5Cfrac%7BH_0%5E2%7D%7BG_N%5Cbar%7Bm%7D%7D%20%5Cright%20%29%2C%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%2811%29)

![equation](http://latex.codecogs.com/gif.latex?-%5Cfrac%7B1%7D%7B%5Cbar%7Bm%7D%5Ctilde%7Ba%7D%5E3%7D%5Csum_%7Bj%5Cneq%20i%7D%5Cfrac%7Bm_j%5Cleft%28%5Chat%7By%7D_i-%5Chat%7By%7D_j%20%5Cright%29%7D%7B%5Cleft%5B%5Cleft%28%5Chat%7Bx%7D_i-%5Chat%7Bx%7D_j%20%5Cright%20%29%5E2&plus;%5Cleft%28%20%5Chat%7By%7D_i-%5Chat%7By%7D_j%20%5Cright%20%29%5E2&plus;%5Cleft%28%5Chat%7Bz%7D_i-%5Chat%7Bz%7D_j%20%5Cright%29%5E2%20%5Cright%20%5D%5E%7B3/2%7D%7D%3D%5Cleft%28%5Cfrac%7Bd%5E2%20%5Chat%7By%7D_i%7D%7Bd%5Ctilde%7Bt%7D%5E2%7D&plus;%5Cfrac%7B2%7D%7B%5Ctilde%7Ba%7D%7D%5Cfrac%7Bd%5Ctilde%7Ba%7D%7D%7Bd%5Ctilde%7Bt%7D%7D%5Cfrac%7Bd%5Chat%7By%7D_i%7D%7Bd%5Ctilde%7Bt%7D%7D%5Cright%29l%5E3a_0%5E3%5Cleft%28%5Cfrac%7BH_0%5E2%7D%7BG_N%5Cbar%7Bm%7D%7D%20%5Cright%20%29%2C%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%2812%29)

![equation](http://latex.codecogs.com/gif.latex?-%5Cfrac%7B1%7D%7B%5Cbar%7Bm%7D%5Ctilde%7Ba%7D%5E3%7D%5Csum_%7Bj%5Cneq%20i%7D%5Cfrac%7Bm_j%5Cleft%28%5Chat%7Bz%7D_i-%5Chat%7Bz%7D_j%20%5Cright%29%7D%7B%5Cleft%5B%5Cleft%28%5Chat%7Bx%7D_i-%5Chat%7Bx%7D_j%20%5Cright%20%29%5E2&plus;%5Cleft%28%20%5Chat%7By%7D_i-%5Chat%7By%7D_j%20%5Cright%20%29%5E2&plus;%5Cleft%28%5Chat%7Bz%7D_i-%5Chat%7Bz%7D_j%20%5Cright%29%5E2%20%5Cright%20%5D%5E%7B3/2%7D%7D%3D%5Cleft%28%5Cfrac%7Bd%5E2%20%5Chat%7Bz%7D_i%7D%7Bd%5Ctilde%7Bt%7D%5E2%7D&plus;%5Cfrac%7B2%7D%7B%5Ctilde%7Ba%7D%7D%5Cfrac%7Bd%5Ctilde%7Ba%7D%7D%7Bd%5Ctilde%7Bt%7D%7D%5Cfrac%7Bd%5Chat%7Bz%7D_i%7D%7Bd%5Ctilde%7Bt%7D%7D%5Cright%29l%5E3a_0%5E3%5Cleft%28%5Cfrac%7BH_0%5E2%7D%7BG_N%5Cbar%7Bm%7D%7D%20%5Cright%20%29%2C%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%2813%29)

where

![equation](http://latex.codecogs.com/gif.latex?%5Ctilde%7Bl%7D%3Dla_0%5Cleft%28%5Cfrac%7BH_0%5E2%7D%7BG_N%5Cbar%7Bm%7D%7D%5Cright%29%5E%7B1/3%7D%3D%5Cleft%28%5Cfrac%7B8%5Cpi%20N%7D%7B3%5COmega_M%7D%5Cright%29%5E%7B1/3%7D%2C%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%2814a%29)

![equation](http://latex.codecogs.com/gif.latex?l%3D%5Cfrac%7B1%7D%7Ba_0%7D%5Cleft%28%5Cfrac%7B8%5Cpi%20N%20G_N%5Cbar%7Bm%7D%7D%7B3%5COmega_M%20H_0%5E2%7D%5Cright%29%5E%7B1/3%7D%3D%5Cfrac%7B1%7D%7Ba_0%7D%5Cleft%28%5Cfrac%7B8%5Cpi%20N%20G_N%5Cbar%7Bm%7D%7D%7B3H_0%5E2%7D%5Ccdot%5Cfrac%7B3H_0%5E2a_0%5E3%7D%7B8%5Cpi%20G_N%5Cbar%7B%5Crho%7D%7D%5Cright%29%5E%7B1/3%7D%3D%5Cfrac%7B1%7D%7Ba_0%7D%5Cleft%28%5Cfrac%7BN%5Cbar%7Bm%7Da_0%5E3%7D%7B%5Cbar%7B%5Crho%7D%7D%5Cright%29%5E%7B1/3%7D%2C%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%2814b%29)

i.e.

![equation](http://latex.codecogs.com/gif.latex?l%5E3a_0%5E3%5Cleft%28%5Cfrac%7BH_0%5E2%7D%7BG_N%5Cbar%7Bm%7D%7D%5Cright%29%3D%5Cleft%28%5Cfrac%7B8%5Cpi%20N%7D%7B3%5COmega_M%7D%5Cright%29%2C%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%2815%29)

where N is the total amount of particles. Consequently,

![equation](http://latex.codecogs.com/gif.latex?-%5Cfrac%7B1%7D%7BN%5Cbar%7Bm%7D%5Ctilde%7Ba%7D%5E3%7D%5Csum_%7Bj%5Cneq%20i%7D%5Cfrac%7Bm_j%5Cleft%28%5Chat%7Bx%7D_i-%5Chat%7Bx%7D_j%20%5Cright%29%7D%7B%5Cleft%5B%5Cleft%28%5Chat%7Bx%7D_i-%5Chat%7Bx%7D_j%20%5Cright%20%29%5E2&plus;%5Cleft%28%20%5Chat%7By%7D_i-%5Chat%7By%7D_j%20%5Cright%20%29%5E2&plus;%5Cleft%28%5Chat%7Bz%7D_i-%5Chat%7Bz%7D_j%20%5Cright%29%5E2%20%5Cright%20%5D%5E%7B3/2%7D%7D%3D%5Cleft%28%5Cfrac%7Bd%5E2%20%5Chat%7Bx%7D_i%7D%7Bd%5Ctilde%7Bt%7D%5E2%7D&plus;%5Cfrac%7B2%7D%7B%5Ctilde%7Ba%7D%7D%5Cfrac%7Bd%5Ctilde%7Ba%7D%7D%7Bd%5Ctilde%7Bt%7D%7D%5Cfrac%7Bd%5Chat%7Bx%7D_i%7D%7Bd%5Ctilde%7Bt%7D%7D%5Cright%29%5Cfrac%7B8%5Cpi%7D%7B3%5COmega_M%7D%2C%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%2816%29)

![equation](http://latex.codecogs.com/gif.latex?-%5Cfrac%7B1%7D%7BN%5Cbar%7Bm%7D%5Ctilde%7Ba%7D%5E3%7D%5Csum_%7Bj%5Cneq%20i%7D%5Cfrac%7Bm_j%5Cleft%28%5Chat%7By%7D_i-%5Chat%7By%7D_j%20%5Cright%29%7D%7B%5Cleft%5B%5Cleft%28%5Chat%7Bx%7D_i-%5Chat%7Bx%7D_j%20%5Cright%20%29%5E2&plus;%5Cleft%28%20%5Chat%7By%7D_i-%5Chat%7By%7D_j%20%5Cright%20%29%5E2&plus;%5Cleft%28%5Chat%7Bz%7D_i-%5Chat%7Bz%7D_j%20%5Cright%29%5E2%20%5Cright%20%5D%5E%7B3/2%7D%7D%3D%5Cleft%28%5Cfrac%7Bd%5E2%20%5Chat%7By%7D_i%7D%7Bd%5Ctilde%7Bt%7D%5E2%7D&plus;%5Cfrac%7B2%7D%7B%5Ctilde%7Ba%7D%7D%5Cfrac%7Bd%5Ctilde%7Ba%7D%7D%7Bd%5Ctilde%7Bt%7D%7D%5Cfrac%7Bd%5Chat%7By%7D_i%7D%7Bd%5Ctilde%7Bt%7D%7D%5Cright%29%5Cfrac%7B8%5Cpi%7D%7B3%5COmega_M%7D%2C%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%2817%29)

![equation](http://latex.codecogs.com/gif.latex?-%5Cfrac%7B1%7D%7BN%5Cbar%7Bm%7D%5Ctilde%7Ba%7D%5E3%7D%5Csum_%7Bj%5Cneq%20i%7D%5Cfrac%7Bm_j%5Cleft%28%5Chat%7Bz%7D_i-%5Chat%7Bz%7D_j%20%5Cright%29%7D%7B%5Cleft%5B%5Cleft%28%5Chat%7Bx%7D_i-%5Chat%7Bx%7D_j%20%5Cright%20%29%5E2&plus;%5Cleft%28%20%5Chat%7By%7D_i-%5Chat%7By%7D_j%20%5Cright%20%29%5E2&plus;%5Cleft%28%5Chat%7Bz%7D_i-%5Chat%7Bz%7D_j%20%5Cright%29%5E2%20%5Cright%20%5D%5E%7B3/2%7D%7D%3D%5Cleft%28%5Cfrac%7Bd%5E2%20%5Chat%7Bz%7D_i%7D%7Bd%5Ctilde%7Bt%7D%5E2%7D&plus;%5Cfrac%7B2%7D%7B%5Ctilde%7Ba%7D%7D%5Cfrac%7Bd%5Ctilde%7Ba%7D%7D%7Bd%5Ctilde%7Bt%7D%7D%5Cfrac%7Bd%5Chat%7Bz%7D_i%7D%7Bd%5Ctilde%7Bt%7D%7D%5Cright%29%5Cfrac%7B8%5Cpi%7D%7B3%5COmega_M%7D%2C%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%2818%29)

Obviously, ![equation](http://latex.codecogs.com/gif.latex?N%5Cbar%7Bm%7D%3Dm_1&plus;m_2&plus;...&plus;m_N%3Dm_%7Btotal%7D) is the total mass of all considered paticles.

## Modelling/Simulation idea

Lets take ![equation](http://latex.codecogs.com/gif.latex?N%5Cgg1) (e.g. 10 000 or more) particles and model their movements in the "box": ![equation](http://latex.codecogs.com/gif.latex?0%5Cleq%5Chat%7Bx%7D_i%2C%5Chat%7By%7D_i%2C%5Chat%7Bz%7D_i%5Cleq1), (leaving the considered region of space on the one hand, the particle gets into it with the opposite one, as in standard molecular dynamics). The distance between the particles is appropriately carried out: ![equation](http://latex.codecogs.com/gif.latex?%5Chat%7Bd%7D_%7Bij%7D%5E2%3D%5Cleft%28%5Chat%7Bx%7D_i-%5Chat%7Bx%7D_j%5Cright%29%5E2&plus;%5Cleft%28%5Chat%7By%7D_i-%5Chat%7By%7D_j%5Cright%29%5E2&plus;%5Cleft%28%5Chat%7Bz%7D_i-%5Chat%7Bz%7D_j%5Cright%29%5E2) while ![equation](http://latex.codecogs.com/gif.latex?%7C%5Chat%7Bx%7D_i-%5Chat%7Bx%7D_j%7C%5Cleq1/2) and ![equation](http://latex.codecogs.com/gif.latex?%5Chat%7Bd%7D_%7Bij%7D%5E2%3D%5Cleft%281-%7C%5Chat%7Bx%7D_i-%5Chat%7Bx%7D_j%7C%5Cright%29%5E2&plus;%5Cleft%28%5Chat%7By%7D_i-%5Chat%7By%7D_j%5Cright%29%5E2&plus;%5Cleft%28%5Chat%7Bz%7D_i-%5Chat%7Bz%7D_j%5Cright%29%5E2) while ![equation](http://latex.codecogs.com/gif.latex?%7C%5Chat%7Bx%7D_i-%5Chat%7Bx%7D_j%7C%5Cgeq1/2) (the same for other coordinates). The numerators of the fractions in the right-hand sides of equations (5), (6), and (7) must also have the form ![equation](http://latex.codecogs.com/gif.latex?%5Chat%7Bx%7D_i-%5Chat%7Bx%7D_j) or ![equation](http://latex.codecogs.com/gif.latex?%5Cleft%28l-%7C%5Chat%7Bx%7D_i-%5Chat%7Bx%7D_j%7C%5Cright%29%5Cleft%28%5Chat%7Bx%7D_i-%5Chat%7Bx%7D_j%5Cright%29/%7C%5Chat%7Bx%7D_i-%5Chat%7Bx%7D_j%7C). In addition, in the denominators of fractions, we should add a small additive number, which eliminates possible divergences in the unrestricted approximation of particles, but does not significantly affect the simulation because of its smallness.

The numerators of the fractions in the right-hand sides of equations (5), (6) and (7) must also have the form (formula) or (formula). In addition, in the denominators of fractions it is necessary to add a small additive number, removing possible divergences for an unrestricted approach of particles, but not significantly affecting because of their smallness.
If we take the modern average value of a typical pecular velocity equal to 300 km/s, then it will be $0.1\%$ of the speed of light $c$. Given the inverse relationship between this relationship and the large-scale factor a, we can expect that the developed mechanical approach is valid until the moment when this ratio is, for example, $1\%$. In accordance with (4), this will happen when (formula). Exactly this moment of the past can be taken as the original one.

The initial (at the found moment of the past) distribution of particles with respect to the spatial coordinates, velocities and masses is given "by hands". For example, the x-component is "physical" pecular velocity vph x = adx = dt = H0a0 ?? adx = d ?? t = H0a0l ?? ad? x = d ?? t, whence d € x = d ?? t = vphx = (H0a0l ?? a). Initial velocities d should be taken about this value, where ?? a = 0: 1, and as vph x it is necessary to take value (formula).

## Numerical Integration

Equations (16) - (18) can be integrated numerically as follows. At the initial time ?? tin = -0: 95, the coordinates of the particles € xi ( ?? tin) and the velocity d € xi ( ?? tin ) = d ?? t. From the equations (16) - (18) find initial accelerations d2 € xi ( ?? tin ) = d ?? t2. Then, according to the initial velocities and accelerations, and the velocity of the particles at time tin +. ?? t:

![equation](http://latex.codecogs.com/gif.latex?%5Chat%7Bx%7D_i%5Cleft%28%5Ctilde%7Bt%7D_%7Bin%7D&plus;%5CDelta%5Ctilde%7Bt%7D%5Cright%29%3D%5Cfrac%7Bd%5Chat%7Bx%7D_i%5Cleft%28%5Ctilde%7Bt%7D_%7Bin%7D%5Cright%29%7D%7Bd%5Ctilde%7Bt%7D%7D%5CDelta%5Ctilde%7Bt%7D%2C)

![equation](http://latex.codecogs.com/gif.latex?%5Cfrac%7Bd%5Chat%7Bx%7D_i%5Cleft%28%5Ctilde%7Bt%7D_%7Bin%7D&plus;%5CDelta%5Ctilde%7Bt%7D%5Cright%29%7D%7Bd%5Ctilde%7Bt%7D%7D%3D%5Cfrac%7Bd%5E2%5Chat%7Bx%7D_i%5Cleft%28%5Ctilde%7Bt%7D_%7Bin%7D%5Cright%29%7D%7Bd%5Ctilde%7Bt%7D%5E2%7D%5CDelta%5Ctilde%7Bt%7D%2C)

## Examples

**We can choose either same type of galaxies...**

<p align="center">
  <img src="/examples/same_galaxies.gif?raw=true" width="800px">
</p>

**or a different one**

<p align="center">
  <img src="/examples/different_galaxies.gif?raw=true" width="800px">
</p>

**As the space stretches itself...**

<p align="center">
  <img src="/examples/disappearing_galaxies1.gif?raw=true" width="800px"/>
</p>

**...galaxies should eventually dissapear from view**

<p align="center">
  <img src="/examples/disappearing_galaxies2.gif?raw=true" width="800px">
</p>
