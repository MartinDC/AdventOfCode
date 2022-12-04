import {  AoCComponent } from "../../core/defs";

/**
 * Advent of Code 2019
 * Solution for Day 1. Part 1
 */

 export class AoCSolutionDay1Part1 extends AoCComponent {

    private calculateWeightFuelConsumption(moduleWeight: number) {
        return Math.floor(moduleWeight / 3) - 2;
    }

    public solve(): number {
        let calculatedSum = 0;
        this.tokens.forEach((token) => {
            const calculatedModuleWeight = this.calculateWeightFuelConsumption(token.integer);
            calculatedSum +=  calculatedModuleWeight;
        })
        return calculatedSum;
    }
}