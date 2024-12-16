# Comparing Object-Oriented and Data-Oriented Programming for Performance

In this project, I explore the performance difference between **Object-Oriented Programming (OOP)** and **Data-Oriented Programming (DOP)** by simulating a scenario with 500,000 cubes.

The goal is to demonstrate how DOP can significantly improve performance over traditional OOP.

## Problem Setup
- **Scenario**: Spawn 500,000 cubes in a 3D space.
- **Task**: Check if each cube is inside a defined bounding box.
- **Comparison**: Implement the solution first using Object-Oriented techniques, then refactor it using a Data-Oriented approach.

## Performance Metrics
The following metrics were measured while benchmarking the Seek function:

| Implementation Approach | Time Taken (ms)
|--------------------------|-----------------|
| Object-Oriented         | 50ms             |
| Object-Oriented Multi-Thread         | 26ms             |
| Data-Oriented           | 35ms              |
| Data-Oriented Multi-Thread           | 0.64ms              |

Data oriented and Single-Thread version should've been faster but for some special reason it is not. With different math approach it can be measured **15ms**.

## Visual Showcase
Below is a visual representation of the project: 
- A yellow cube (the Seeker) rotates around the world's origin, represented by a green cube.
- While rotating, the Seeker evaluates 500,000 cubes to check if any fall inside a specified bounding box during each frame.
- Cubes that are inside the bounding box for a given frame are highlighted in red.

![Performance Showcase](https://i.giphy.com/media/v1.Y2lkPTc5MGI3NjExb21qcW95NTRhdW02aDQzMWlqdW5jZmM0emJ4cjJ1NGM2ZnN4ZThxeiZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/C2LbEnrtTK1I7Vt1as/giphy.gif)

## Conclusion
The project illustrates the power of Data-Oriented Programming in high-performance scenarios. By restructuring data for better memory access patterns, the Data-Oriented approach achieves significant performance.