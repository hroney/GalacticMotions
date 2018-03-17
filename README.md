# GalacticMotions

> This project is based on the paper arXiv:1211.4045. The main idea is to visualize the dynamical behavior of astrophysical objects 
(e.g. galaxies) taking into account both the gravitational attraction between them and the cosmological expansion of the Universe

## Theory

We consider astrophysical objects deep inside the cell of uniformity (i.e. $R<150$Mpc). The comoving distances in the cell of uniformity are much less than 1: $li ≪ 1$ -> we can use the Cartesian coordinates. Thus, we can write

![equation](http://latex.codecogs.com/gif.latex?-%5Cfrac%7B1%7D%7B%5Cbar%7Bm%7D%7D%5Csum_%7Bj%5Cneq%20i%7D%5Cfrac%7Bm_j%5Cleft%28%5Ctilde%7BX%7D_i-%5Ctilde%7BX%7D_j%20%5Cright%29%7D%7B%5Cleft%5B%5Cleft%28%5Ctilde%7BX%7D_i-%5Ctilde%7BX%7D_j%20%5Cright%20%29%5E2&plus;%5Cleft%28%20%5Ctilde%7BY%7D_i-%5Ctilde%7BY%7D_j%20%5Cright%20%29%5E2&plus;%5Cleft%28%5Ctilde%7BZ%7D_i-%5Ctilde%7BZ%7D_j%20%5Cright%29%5E2%20%5Cright%20%5D%7D%3D%5Cfrac%7B1%7D%7B%5Ctilde%7Ba%7D%7D%5Cleft%28%5Cfrac%7Bd%5E2%20%5Ctilde%7BX%7D_i%7D%7Bd%5Ctilde%7Bt%7D%5E2%7D%5Ctilde%7Ba%7D-%5Cfrac%7Bd%5E2%5Ctilde%7Ba%7D%7D%7Bd%5Ctilde%7Bt%7D%5E2%7D%5Ctilde%7BX%7D_i%5Cright%29%2C%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%281%29)

![equation](http://latex.codecogs.com/gif.latex?-%5Cfrac%7B1%7D%7B%5Cbar%7Bm%7D%7D%5Csum_%7Bj%5Cneq%20i%7D%5Cfrac%7Bm_j%5Cleft%28%5Ctilde%7BY%7D_i-%5Ctilde%7BY%7D_j%20%5Cright%29%7D%7B%5Cleft%5B%5Cleft%28%5Ctilde%7BX%7D_i-%5Ctilde%7BX%7D_j%20%5Cright%20%29%5E2&plus;%5Cleft%28%20%5Ctilde%7BY%7D_i-%5Ctilde%7BY%7D_j%20%5Cright%20%29%5E2&plus;%5Cleft%28%5Ctilde%7BZ%7D_i-%5Ctilde%7BZ%7D_j%20%5Cright%29%5E2%20%5Cright%20%5D%7D%3D%5Cfrac%7B1%7D%7B%5Ctilde%7Ba%7D%7D%5Cleft%28%5Cfrac%7Bd%5E2%20%5Ctilde%7BY%7D_i%7D%7Bd%5Ctilde%7Bt%7D%5E2%7D%5Ctilde%7Ba%7D-%5Cfrac%7Bd%5E2%5Ctilde%7Ba%7D%7D%7Bd%5Ctilde%7Bt%7D%5E2%7D%5Ctilde%7BY%7D_i%5Cright%29%2C%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%282%29)

![equation](http://latex.codecogs.com/gif.latex?-%5Cfrac%7B1%7D%7B%5Cbar%7Bm%7D%7D%5Csum_%7Bj%5Cneq%20i%7D%5Cfrac%7Bm_j%5Cleft%28%5Ctilde%7BZ%7D_i-%5Ctilde%7BZ%7D_j%20%5Cright%29%7D%7B%5Cleft%5B%5Cleft%28%5Ctilde%7BX%7D_i-%5Ctilde%7BX%7D_j%20%5Cright%20%29%5E2&plus;%5Cleft%28%20%5Ctilde%7BY%7D_i-%5Ctilde%7BY%7D_j%20%5Cright%20%29%5E2&plus;%5Cleft%28%5Ctilde%7BZ%7D_i-%5Ctilde%7BZ%7D_j%20%5Cright%29%5E2%20%5Cright%20%5D%7D%3D%5Cfrac%7B1%7D%7B%5Ctilde%7Ba%7D%7D%5Cleft%28%5Cfrac%7Bd%5E2%20%5Ctilde%7BZ%7D_i%7D%7Bd%5Ctilde%7Bt%7D%5E2%7D%5Ctilde%7Ba%7D-%5Cfrac%7Bd%5E2%5Ctilde%7Ba%7D%7D%7Bd%5Ctilde%7Bt%7D%5E2%7D%5Ctilde%7BZ%7D_i%5Cright%29%2C%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%5Cquad%20%283%29)

## Modelling/Simulation idea

Lets take $N>>1$ (e.g. 10 000 or more) particles and model their movements in the "box": $0<x,y,z<1$, (leaving the considered region of space on the one hand, the particle gets into it with the opposite one, as in standard molecular dynamics). The distance between the particles is appropriately carried out

(formula)

The numerators of the fractions in the right-hand sides of equations (5), (6) and (7) must also have the form (formula) or (formula). In addition, in the denominators of fractions it is necessary to add a small additive number, removing possible divergences for an unrestricted approach of particles, but not significantly affecting because of their smallness.
If we take the modern average value of a typical pecular velocity equal to 300 km/s, then it will be $0.1\%$ of the speed of light $c$. Given the inverse relationship between this relationship and the large-scale factor a, we can expect that the developed mechanical approach is valid until the moment when this ratio is, for example, $1\%$. In accordance with (4), this will happen when (formula). Exactly this moment of the past can be taken as the original one.
The initial (at the found moment of the past) distribution of particles with respect to the spatial coordinates, velocities and masses is given "by hands". For example, the x-component is "physical" pecular velocity vph x = adx = dt = H0a0 ?? adx = d ?? t = H0a0l ?? ad? x = d ?? t, whence d € x = d ?? t = vphx = (H0a0l ?? a). Initial velocities d should be taken about this value, where ?? a = 0: 1, and as vph x it is necessary to take value (formula).

Equations (16) - (18) can be integrated numerically as follows. At the initial time ?? tin = -0: 95, the coordinates of the particles € xi ( ?? tin) and the velocity d € xi ( ?? tin ) = d ?? t. From the equations (16) - (18) find initial accelerations d2 € xi ( ?? tin ) = d ?? t2. Then, according to the initial velocities and accelerations, and the velocity of the particles at time tin +. ?? t:

(formula)

## Examples

