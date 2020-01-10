import {  AoCComponent } from "../../core/defs";

/**
 * Advent of Code 2019
 * Solution for Day 1. Part 2
 */

export class AoCSolutionDay1Part2 extends AoCComponent {

    private calculateWeightFuelConsumption(moduleWeight: number) {
        return Math.floor(moduleWeight / 3) - 2;
    }

    /** The extra fuel carried by a module must be included in the calculation for part two. */
    private calculateFuelOwnConsumption(fuelWeight: number, accumulator: number): number {
        const weight = this.calculateWeightFuelConsumption(fuelWeight);
        if (weight > 0) { return this.calculateFuelOwnConsumption(weight, weight + accumulator); }
        if (weight <= 0) { return accumulator; }
    }

    public solve(): number {
        let calculatedSum = 0;
        this.tokens.forEach((token) => {
            const calculatedModuleWeight = this.calculateWeightFuelConsumption(token.integer);
            const addedFuelWeight = this.calculateFuelOwnConsumption(calculatedModuleWeight, 0);
            calculatedSum += calculatedModuleWeight + addedFuelWeight;
        })
        return calculatedSum;
    }
}